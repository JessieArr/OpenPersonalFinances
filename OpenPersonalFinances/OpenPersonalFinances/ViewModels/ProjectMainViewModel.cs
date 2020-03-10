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
using System.Threading.Tasks;

namespace OpenPersonalFinances.ViewModels
{
    public class ProjectMainViewModel : ViewModelBase
    {
        public OPFProject Project { get; set; }
        public string FilterText { get; set; }
        public ObservableCollection<ListRecord> List { get; set; } = new ObservableCollection<ListRecord>();
        ViewModelBase content;

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public ProjectMainViewModel(OPFProject project)
        {
            Project = project;            
            CurrentProjectService.ActiveAccountChanged += ActiveAccountChanged;
            UpdateListContents();
        }

        private void ActiveAccountChanged(object sender, OPFAccount e)
        {
        }

        public void CreateAccount()
        {
            var window = new NewAccountDialog();
            window.Show();
        }

        public async Task ImportRecords()
        {
            var dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Comma Separated Value Files", Extensions = { "csv" } });

            var result = await dialog.ShowAsync(new Window());

            if (result.Any())
            {
                var filePath = result.First();
                var fileContents = File.ReadAllText(filePath);
                var values = fileContents.Split('\n');
                CurrentProjectService.ActiveAccount.Records = values.ToList();
                UpdateListContents();
            }
        }

        public void FilterRecords()
        {
            UpdateListContents();
        }

        private void UpdateListContents()
        {
            List.Clear();
            if(CurrentProjectService.ActiveAccount == null)
            {
                foreach (var account in Project.Accounts)
                {
                    var filteredRecords = account.Records.Skip(1);
                    if(!String.IsNullOrEmpty(FilterText))
                    {
                        filteredRecords = filteredRecords.Where(x => x.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    foreach (var record in filteredRecords)
                    {
                        if(String.IsNullOrEmpty(record))
                        {
                            continue;
                        }
                        List.Add(new ListRecord(record));
                    }
                }
            }
            else
            {
                var filteredRecords = CurrentProjectService.ActiveAccount.Records.Skip(1);
                if (!String.IsNullOrEmpty(FilterText))
                {
                    filteredRecords = filteredRecords.Where(x => x.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                foreach (var record in filteredRecords)
                {
                    if (String.IsNullOrEmpty(record))
                    {
                        continue;
                    }
                    List.Add(new ListRecord(record));
                }
            }
            
        }
    }
}