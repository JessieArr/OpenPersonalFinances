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
    }
}
