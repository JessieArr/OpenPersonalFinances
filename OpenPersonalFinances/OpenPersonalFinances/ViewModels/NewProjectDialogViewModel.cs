using Avalonia.Controls;
using Newtonsoft.Json;
using OpenPersonalFinances.Models;
using OpenPersonalFinances.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenPersonalFinances.ViewModels
{
    public class NewProjectDialogViewModel : ViewModelBase
    {
        private string _FolderPath;
        private Window _ParentWindow;
        public string NewProjectName { get; set; } = "MyProject.opfi";

        public NewProjectDialogViewModel(string path, Window parentWindow)
        {
            _FolderPath = path;
            _ParentWindow = parentWindow;
        }

        public void NewProjectCommand()
        {
            CurrentProjectService.ActiveProject = new OPFProject();
            var projectText = JsonConvert.SerializeObject(CurrentProjectService.ActiveProject);
            File.WriteAllText(Path.Join(_FolderPath, NewProjectName), projectText);
            _ParentWindow.Close();
        }
    }
}
