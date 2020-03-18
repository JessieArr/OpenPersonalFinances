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
            SUT.GetColumnConfigForCSV(testCsv);
        }
    }
}
