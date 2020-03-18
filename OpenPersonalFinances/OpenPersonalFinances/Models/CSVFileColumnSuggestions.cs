using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class CSVFileColumnSuggestions
    {
        public List<string> DateColumns { get; set; } = new List<string>();
        public List<string> StringColumns { get; set; } = new List<string>();
        public List<string> AmountColumns { get; set; } = new List<string>();
    }
}
