using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class OPFProject
    {
        public List<OPFAccount> Accounts { get; set; } = new List<OPFAccount>();
        public List<AccountRecord> Transactions { get; set; } = new List<AccountRecord>();
    }
}
