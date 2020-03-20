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
            var splitHeaders = SplitCSVRow(headers);
            var rows = file.Rows;
            var columnData = new Dictionary<string, List<string>>();
            foreach(var header in splitHeaders)
            {
                columnData[header] = new List<string>();
            }
            foreach(var row in rows)
            {
                var splitRow = SplitCSVRow(row);
                if(splitHeaders.Count != splitRow.Count)
                {
                    throw new Exception("CSV data was malformed");
                }
                var rowDictionary = new Dictionary<string, string>();
                for(var i = 0; i < splitHeaders.Count; i++)
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

        public List<AccountRecord> GetRecordsForCSVFile(CSVFile file, CSVFileColumnOptions columnOptions, int accountId)
        {
            var splitHeaders = SplitCSVRow(file.Header);
            var dateColumn = 0;
            var amountColumns = new Dictionary<int, bool>();
            var categoryColumn = 0;
            var descriptionColumn = 0;
            for(var i = 0; i < splitHeaders.Count; i++)
            {
                if(String.Equals(splitHeaders[i], columnOptions.DateColumn, StringComparison.OrdinalIgnoreCase))
                {
                    dateColumn = i;
                }                
                if (String.Equals(splitHeaders[i], columnOptions.CategoryColumn, StringComparison.OrdinalIgnoreCase))
                {
                    categoryColumn = i;
                }
                if (String.Equals(splitHeaders[i], columnOptions.DescriptionColumn, StringComparison.OrdinalIgnoreCase))
                {
                    descriptionColumn = i;
                }
                
                if (columnOptions.AmountColumns.Any(x => String.Equals(splitHeaders[i], x.Key, StringComparison.OrdinalIgnoreCase)))
                {
                    var matchingAmountColumn = columnOptions.AmountColumns.First(x => String.Equals(splitHeaders[i], x.Key, StringComparison.OrdinalIgnoreCase));
                    amountColumns.Add(i, matchingAmountColumn.Value);
                }
            }

            var records = new List<AccountRecord>();
            foreach(var row in file.Rows)
            {
                var splitRow = SplitCSVRow(row);
                var newRecord = new AccountRecord();
                newRecord.AccountID = accountId;
                newRecord.Date = DateTime.Parse(splitRow[dateColumn]);
                newRecord.Category = splitRow[categoryColumn];
                newRecord.Description = splitRow[descriptionColumn];

                var finalValue = 0f;
                foreach(var amountColumn in amountColumns)
                {
                    var amountString = splitRow[amountColumn.Key];
                    if(String.IsNullOrEmpty(amountString))
                    {
                        continue;
                    }
                    var value = float.Parse(splitRow[amountColumn.Key]);
                    if(amountColumn.Value)
                    {
                        value = value * -1;
                    }
                    finalValue += value;
                }
                newRecord.Amount = finalValue;

                records.Add(newRecord);
            }

            return records;
        }

        public List<string> SplitCSVRow(string row)
        {
            var result = new List<string>();
            var segmentStartIndex = 0;
            var isInQuoteBlock = false;
            for (var i = 0; i < row.Length; i++)
            {
                if(row[i] == '"')
                {
                    isInQuoteBlock = !isInQuoteBlock;
                }
                if(row[i] == ',' && !isInQuoteBlock)
                {
                    result.Add(row.Substring(segmentStartIndex, i - segmentStartIndex));
                    segmentStartIndex = i + 1;
                }
                if(i == row.Length - 1)
                {
                    result.Add(row.Substring(segmentStartIndex));
                }
            }
            if (result.All(x => x.StartsWith("\"") && x.EndsWith("\"")))
            {
                result = result.Select(x => x.Substring(1, x.Length - 2)).ToList();
            }
            return result;
        }
    }
}
