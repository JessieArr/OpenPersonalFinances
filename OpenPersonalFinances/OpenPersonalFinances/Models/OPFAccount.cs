using OpenPersonalFinances.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class OPFAccount
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Headers { get; set; }

        public void SelectAccount()
        {
            CurrentProjectService.ActiveAccount = this;
        }
    }
}
