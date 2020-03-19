using OpenPersonalFinances.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OpenPersonalFinances.Test.SystemTests
{
    public class CSVFileServiceTests
    {
        private static string _chaseFormatCSVFile = "./Data/chase.csv";
        private static string _payTrustFormatCSVFile = "./Data/PayTrust.csv";
        private static string _payPalFormatCSVFile = "./Data/PayPal.csv";

        private CSVFileService SUT;
        public CSVFileServiceTests()
        {
            SUT = new CSVFileService();
        }

        [Fact]
        public void OpenCSVFile_DoesNotThrow()
        {
            var csv = SUT.OpenCSVFile(_chaseFormatCSVFile);
        }

        [Theory]
        [MemberData(nameof(GetAllFiles))]
        public void OpenCSVFile_SetsValues(string fileName, int rowCount)
        {
            var csv = SUT.OpenCSVFile(fileName);

            Assert.NotNull(csv.Header);
            Assert.NotNull(csv.Rows);
            Assert.Equal(rowCount, csv.Rows.Count);
        }

        public static IEnumerable<object[]> GetAllFiles()
        {
            var allData = new List<object[]>
            {
                new object[] { _chaseFormatCSVFile, 2 },
                new object[] { _payTrustFormatCSVFile, 2 },
                new object[] { _payPalFormatCSVFile , 2},
            };

            return allData;
        }
    }
}
