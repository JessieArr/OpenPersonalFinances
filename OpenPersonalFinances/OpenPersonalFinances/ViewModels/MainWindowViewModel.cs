using Avalonia.Controls;
using OpenPersonalFinances.Models;
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
        public ObservableCollection<RowItem> Items { get; set; } = new ObservableCollection<RowItem>()
        {
            new RowItem("One"),
            new RowItem("Two"),
            new RowItem("Three"),
            new RowItem("Four"),
        };

        public async Task OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "CSV Files", Extensions = { "csv" } });

            var result = await dialog.ShowAsync(new Window());

            if (result.Any())
            {
                var filePath = result.First();
                var fileContents = File.ReadAllText(filePath);
                var rows = fileContents.Split('\n');
                Items.Clear();
                foreach (var row in rows)
                {
                    Items.Add(new RowItem(row));
                }
            }
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
