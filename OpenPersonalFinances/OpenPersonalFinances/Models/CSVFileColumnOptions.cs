using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class CSVFileColumnOptions
    {
        public string DateColumn { get; set; }
        public Dictionary<string, bool> AmountColumns { get; set; } = new Dictionary<string, bool>();
        public string CategoryColumn { get; set; }
        public string DescriptionColumn { get; set; }
    }
}
