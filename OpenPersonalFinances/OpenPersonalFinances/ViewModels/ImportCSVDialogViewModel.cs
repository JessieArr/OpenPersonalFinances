using Avalonia.Controls;
using OpenPersonalFinances.Models;
using OpenPersonalFinances.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenPersonalFinances.ViewModels
{
    public class ImportCSVDialogViewModel
    {
        private Window _ParentWindow;
        public string Headers { get; set; }
        public List<string> PreviewItems { get; set; } = new List<string>()
        {
            "One",
            "Two",
            "Three"
        };
        public List<string> AmountColumns { get; set; } = new List<string>();
        public List<string> DateColumns { get; set; } = new List<string>();
        public List<string> StringColumns { get; set; } = new List<string>();
        public string DateColumn { get; set; }
        public string AmountColumn { get; set; }
        public string DescriptionColumn { get; set; }
        public string CategoryColumn { get; set; }

        public ImportCSVDialogViewModel(Window parentWindow, CSVFile file)
        {
            _ParentWindow = parentWindow;
            Headers = file.Header;
            PreviewItems = file.Rows.Take(10).ToList();
            var service = new CSVFileService();
            var columnSuggestions = service.GetColumnSuggestionsForCSV(file);
            AmountColumns = columnSuggestions.AmountColumns;
            DateColumns = columnSuggestions.DateColumns;
            StringColumns = columnSuggestions.StringColumns;
        }

        public void ImportCommand()
        {
            _ParentWindow.Close();
        }
    }
}
