using OpenPersonalFinances.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Services
{
    public static class CurrentProjectService
    {
        private static OPFProject _ActiveProject;
        public static OPFProject ActiveProject
        {
            get
            {
                return _ActiveProject;
            }
            set
            {
                _ActiveProject = value;
                ActiveProjectChanged(null, value);
            }
        }
        public static event EventHandler<OPFProject> ActiveProjectChanged;

        private static OPFAccount _ActiveAccount;
        public static OPFAccount ActiveAccount
        {
            get
            {
                return _ActiveAccount;
            }
            set
            {
                _ActiveAccount = value;
                ActiveAccountChanged(null, value);
            }
        }
        public static event EventHandler<OPFAccount> ActiveAccountChanged;
        public static string ActiveProjectPath { get; set; }
    }
}
