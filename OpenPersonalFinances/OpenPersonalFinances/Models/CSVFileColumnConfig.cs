using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class CSVFileColumnConfig
    {
        public string DateColumn { get; set; }
        public string DescriptionColumn { get; set; }
        public string CategoryColumn { get; set; }
        public Dictionary<string, bool> AmountColumns { get; set; } = new Dictionary<string, bool>();
    }
}
