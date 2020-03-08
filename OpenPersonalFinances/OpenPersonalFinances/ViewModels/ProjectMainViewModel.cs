using OpenPersonalFinances.Models;
using OpenPersonalFinances.Windows;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenPersonalFinances.ViewModels
{
    public class ProjectMainViewModel : ViewModelBase
    {
        public OPFProject Project { get; set; }
        public ObservableCollection<string> List { get; set; }

        public ProjectMainViewModel(OPFProject project)
        {
            Project = project;
        }

        public void CreateAccount()
        {
            var window = new NewAccountDialog();
            window.Show();
        }
    }
}