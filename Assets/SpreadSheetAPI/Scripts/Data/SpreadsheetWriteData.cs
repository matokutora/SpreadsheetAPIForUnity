using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spreadsheet.Data
{
    public struct SpreadsheetWriteData
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public string Value { get; set; }
    }
}
