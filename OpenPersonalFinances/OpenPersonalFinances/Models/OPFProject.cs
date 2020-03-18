using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class OPFProject
    {
        public List<OPFAccount> Accounts { get; set; } = new List<OPFAccount>();
        public List<AccountRecord> Transactions { get; set; } = new List<AccountRecord>();

        public int AddRecordsExceptDuplicates(List<AccountRecord> newRecords)
        {
            var duplicateCount = 0;
            foreach(var record in newRecords)
            {
                if(!Transactions.Any(x => x.Date == record.Date && x.Amount == record.Amount
                    && x.Category == record.Category && x.Description == record.Description))
                {
                    Transactions.Add(record);
                }
                else
                {
                    duplicateCount++;
                }
            }
            return duplicateCount;
        }
    }
}
