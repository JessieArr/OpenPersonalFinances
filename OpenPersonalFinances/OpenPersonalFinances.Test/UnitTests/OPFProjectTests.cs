using OpenPersonalFinances.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OpenPersonalFinances.Test.UnitTests
{
    public class OPFProjectTests
    {
        private OPFProject SUT;

        private List<AccountRecord> _testRecords;

        public OPFProjectTests()
        {
            SUT = new OPFProject();

            _testRecords = new List<AccountRecord>() { new AccountRecord()
            {
                AccountID = 13,
                Amount = 12.34f,
                Date = new DateTime(2000, 2, 3),
                Category = "Test Category",
                Description = "Test Description"
            }
            };
        }

        [Fact]
        public void AddRecordsExceptDuplicates_DoesNotThrow()
        {
            SUT.AddRecordsExceptDuplicates(_testRecords);
        }

        [Fact]
        public void AddRecordsExceptDuplicates_AddsRecords()
        {
            Assert.Empty(SUT.Transactions);
            SUT.AddRecordsExceptDuplicates(_testRecords);
            Assert.NotEmpty(SUT.Transactions);
        }

        [Fact]
        public void AddRecordsExceptDuplicates_SkipsDuplicates()
        {
            Assert.Empty(SUT.Transactions);
            var firstDuplicateCount = SUT.AddRecordsExceptDuplicates(_testRecords);
            var secondDuplicateCount = SUT.AddRecordsExceptDuplicates(_testRecords);
            Assert.Equal(0, firstDuplicateCount);
            Assert.Equal(1, secondDuplicateCount);
            Assert.Single(SUT.Transactions);
        }
    }
}
