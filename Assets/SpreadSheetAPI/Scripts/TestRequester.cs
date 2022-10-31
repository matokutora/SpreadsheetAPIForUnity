using UnityEngine;
using UnityEngine.Networking;
using Spreadsheet;
using Spreadsheet.API;
using Spreadsheet.Data;

public class TestRequester : MonoBehaviour
{
    [SerializeField] SpreadsheetDataAsset _dataAsset;
    [SerializeField] int _sheetIndex;
    
    void Start()
    {
        SpreadsheetAPI.SendCreateJsonDataModel(_dataAsset, _sheetIndex);
        SpreadsheetAPI.SendCreateJsonFile(_dataAsset, _sheetIndex);
    }
}
