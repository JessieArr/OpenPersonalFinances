using OpenPersonalFinances.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Services
{
    public static class CurrentProjectService
    {
        private static OPFProject _ActiveProject;
        public static OPFProject ActiveProject { 
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
        public static string ActiveProjectPath { get; set; }
    }
}
