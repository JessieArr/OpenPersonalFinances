using Avalonia.Controls;
using Newtonsoft.Json;
using OpenPersonalFinances.Models;
using OpenPersonalFinances.Services;
using OpenPersonalFinances.Windows;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPersonalFinances.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public async Task NewProject()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(new Window());

            if (!String.IsNullOrEmpty(result))
            {
                var window = new NewProjectDialog(result);
                window.Show();
            }
        }

        public async Task OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Open Personal Finance Projects", Extensions = { "opfi" } });

            var result = await dialog.ShowAsync(new Window());

            if (result.Any())
            {
                var filePath = result.First();
                var fileContents = File.ReadAllText(filePath);
                CurrentProjectService.ActiveProject = JsonConvert.DeserializeObject<OPFProject>(fileContents);
            }
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
