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
        private CSVFileService _csvFileService;
        private CSVFile _csvFile;

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
        public bool AmountColumnInverted { get; set; }
        public string SecondaryAmountColumn { get; set; }
        public bool SecondaryAmountColumnInverted { get; set; }
        public string DescriptionColumn { get; set; }
        public string CategoryColumn { get; set; }

        public ImportCSVDialogViewModel(Window parentWindow, CSVFile file)
        {
            _ParentWindow = parentWindow;
            _csvFileService = new CSVFileService();
            _csvFile = file;

            Headers = file.Header;
            PreviewItems = file.Rows.Take(10).ToList();
            var columnSuggestions = _csvFileService.GetColumnSuggestionsForCSV(file);
            AmountColumns = columnSuggestions.AmountColumns;
            DateColumns = columnSuggestions.DateColumns;
            StringColumns = columnSuggestions.StringColumns;
        }

        public void ImportCommand()
        {
            var columnOptions = new CSVFileColumnOptions()
            {
                DateColumn = DateColumn,
                CategoryColumn = CategoryColumn,
                DescriptionColumn = DescriptionColumn
            };
            if(!String.IsNullOrEmpty(AmountColumn))
            {
                columnOptions.AmountColumns.Add(AmountColumn, AmountColumnInverted);
            }
            if (!String.IsNullOrEmpty(SecondaryAmountColumn))
            {
                columnOptions.AmountColumns.Add(SecondaryAmountColumn, SecondaryAmountColumnInverted);
            }

            var newRecords = _csvFileService.GetRecordsForCSVFile(_csvFile, columnOptions, 0);
            var duplicateCount = CurrentProjectService.ActiveProject.AddRecordsExceptDuplicates(newRecords);

            _ParentWindow.Close();
        }
    }
}
