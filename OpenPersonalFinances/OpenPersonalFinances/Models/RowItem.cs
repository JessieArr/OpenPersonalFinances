using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.Models
{
    public class RowItem
    {
        public string Text { get; set; }

        public RowItem(string text)
        {
            Text = text;
        }
    }
}
