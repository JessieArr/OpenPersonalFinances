using OpenPersonalFinances.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class OPFAccount
    {
        public string Name { get; set; }
        public string Headers { get; set; }
        public List<string> Records { get; set; } = new List<string>();

        public void SelectAccount()
        {
            CurrentProjectService.ActiveAccount = this;
        }
    }
}
