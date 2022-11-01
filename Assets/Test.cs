using UnityEngine;
using Spreadsheet;
using Spreadsheet.API;
using Spreadsheet.Data;

public class Test : MonoBehaviour, ISpreadsheetWrite
{
    [SerializeField] SpreadsheetDataAsset _dataAsset;

    void Start()
    {
        SpreadsheetWriter writer = new SpreadsheetWriter(this);
        writer.Request(_dataAsset, 0);
    }

    public void SetWrite(out SpreadsheetWriteData data)
    {
        data = new SpreadsheetWriteData();
        data.Column = 3;
        data.Row = 1;
        data.Value = "Test";
    }
}
