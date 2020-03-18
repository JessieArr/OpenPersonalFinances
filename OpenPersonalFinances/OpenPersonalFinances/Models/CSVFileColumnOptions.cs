using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class CSVFileColumnOptions
    {
        public string DateColumn { get; set; }
        public List<string> AmountColumns { get; set; } = new List<string>();
        public string CategoryColumn { get; set; }
        public string DescriptionColumn { get; set; }
    }
}
