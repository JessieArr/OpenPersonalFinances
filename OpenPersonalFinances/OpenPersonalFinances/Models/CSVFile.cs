using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class CSVFile
    {
        public string Header { get; set; }
        public List<string> Rows { get; set; }
    }
}
