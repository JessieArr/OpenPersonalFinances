using OpenPersonalFinances.Models;
using OpenPersonalFinances.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OpenPersonalFinances.Test.UnitTests
{
    public class CSVFileServiceTests
    {
        private CSVFileService SUT;
        public CSVFileServiceTests()
        {
            SUT = new CSVFileService();
        }

        [Fact]
        public void GetColumnConfigForCSV_DoesNotThrow()
        {
            var testCsv = new CSVFile();
            testCsv.Header = "Transaction Date,Post Date,Description,Category,Type,Amount";
            testCsv.Rows = new List<string>()
            {
                "01/01/2020,01/02/2020,WALMART GROCERY,Groceries,Sale,-12.34"
            };
            SUT.GetColumnSuggestionsForCSV(testCsv);
        }

        [Fact]
        public void GetColumnConfigForCSV_HandlesQuotes()
        {
            var testCsv = new CSVFile();
            testCsv.Header = "\"Date\",\"Time\",\"Time Zone\",\"Description\",\"Currency\",\"Gross\",\"Fee\",\"Net\",\"Balance\",\"Transaction ID\",\"From Email Address\",\"Name\",\"Bank Name\",\"Bank Account\",\"Shipping and Handling Amount\",\"Sales Tax\",\"Invoice ID\",\"Reference Txn ID\"";
            testCsv.Rows = new List<string>()
            {
                "\"1/2/2020\",\"10:40:56\",\"America/Los_Angeles\",\"PreApproved Payment Bill User Payment\",\"USD\",\"-7.00\",\"0.00\",\"-7.00\",\"123.45\",\"9ASDF456ABC\",\"ar@github.com\",\"GitHub, Inc.\",\"\",\"\",\"0.00\",\"0.00\",\"P-123456789\",\"B-ABC1234ABC234567\""
            };
            var result = SUT.GetColumnSuggestionsForCSV(testCsv);

            Assert.NotEmpty(result.StringColumns);
            Assert.NotEmpty(result.DateColumns);
            Assert.NotEmpty(result.AmountColumns);
        }

        [Fact]
        public void GetRecordsForCSVFile_DoesNotThrow()
        {
            var testCsv = new CSVFile();
            testCsv.Header = "Transaction Date,Post Date,Description,Category,Type,Amount";
            testCsv.Rows = new List<string>()
            {
                "01/01/2020,01/02/2020,WALMART GROCERY,Groceries,Sale,-12.34"
            };

            var testColumnOptions = new CSVFileColumnOptions()
            {
                DateColumn = "Transaction Date",
                AmountColumns = new List<string>() { "Amount" },
                CategoryColumn = "Category",
                DescriptionColumn = "Description",
            };

            var result = SUT.GetRecordsForCSVFile(testCsv, testColumnOptions, 13);
        }

        [Fact]
        public void SplitCSVRow_DoesNotThrow()
        {
            var result = SUT.SplitCSVRow("One,Two,Three");
            Assert.Equal(3, result.Count);
            Assert.Equal("One", result[0]);
            Assert.Equal("Two", result[1]);
            Assert.Equal("Three", result[2]);
        }

        [Fact]
        public void SplitCSVRow_HandlesQuotes()
        {
            var result = SUT.SplitCSVRow("One,\"Quoted, Value,\",Three");
            Assert.Equal(3, result.Count);
            Assert.Equal("One", result[0]);
            Assert.Equal("\"Quoted, Value,\"", result[1]);
            Assert.Equal("Three", result[2]);
        }
    }
}
