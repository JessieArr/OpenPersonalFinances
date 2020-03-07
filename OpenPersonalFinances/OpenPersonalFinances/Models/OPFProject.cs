using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class OPFProject
    {
        public List<OPFAccount> Accounts { get; set; } = new List<OPFAccount>();
    }
}
