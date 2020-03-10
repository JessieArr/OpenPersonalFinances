using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class ListRecord
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public float Amount { get; set; }
        public string RawRecord { get; set; }

        public ListRecord(string rawRecord)
        {
            RawRecord = rawRecord;
            var values = rawRecord.Split(',');
            Date = DateTime.Parse(values[0]);
            Description = values[2];
            Category = values[3];
            Amount = float.Parse(values[5]);
        }
    }
}
