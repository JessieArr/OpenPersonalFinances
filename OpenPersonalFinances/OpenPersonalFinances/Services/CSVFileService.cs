using OpenPersonalFinances.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenPersonalFinances.Services
{
    public class CSVFileService
    {
        public CSVFile OpenCSVFile(string filePath)
        {
            var fileContents = File.ReadAllText(filePath);
            var splitContents = fileContents.Split(Environment.NewLine);
            if(splitContents.Length == 1)
            {
                splitContents = fileContents.Split('\n');
            }
            if(splitContents.Length < 2)
            {
                throw new Exception($"File {filePath} was not a valid CSV file.");
            }
            var firstRow = splitContents[0];
            var otherRows = splitContents.Skip(1);

            var returnValue = new CSVFile();
            returnValue.Header = firstRow;
            returnValue.Rows = otherRows.Where(x => !String.IsNullOrEmpty(x)).ToList();
            return returnValue;
        }

        public CSVFileColumnSuggestions GetColumnSuggestionsForCSV(CSVFile file)
        {
            var headers = file.Header;
            var splitHeaders = headers.Split(',');
            var rows = file.Rows;
            var columnData = new Dictionary<string, List<string>>();
            foreach(var header in splitHeaders)
            {
                columnData[header] = new List<string>();
            }
            foreach(var row in rows)
            {
                var splitRow = row.Split(',');
                if(splitHeaders.Length != splitRow.Length)
                {
                    throw new Exception("CSV data was malformed");
                }
                var rowDictionary = new Dictionary<string, string>();
                for(var i = 0; i < splitHeaders.Length; i++)
                {
                    columnData[splitHeaders[i]].Add(splitRow[i]);
                }
            }

            var returnValue = new CSVFileColumnSuggestions();
            var possibleDateColumns = new List<string>();
            var possibleCategoryColumns = new List<string>();
            var possibleDescriptionColumns = new List<string>();

            foreach (var column in columnData)
            {
                var date = DateTime.Now;
                if(column.Value.All(x => DateTime.TryParse(x, out date)))
                {
                    returnValue.DateColumns.Add(column.Key);
                    continue;
                }                

                var myFloat = 0.1f;
                if (column.Value.All(x => String.IsNullOrEmpty(x) || float.TryParse(x, out myFloat)))
                {
                    returnValue.AmountColumns.Add(column.Key);
                    continue;
                }

                returnValue.StringColumns.Add(column.Key);
            }

            return returnValue;
        }
    }
}
