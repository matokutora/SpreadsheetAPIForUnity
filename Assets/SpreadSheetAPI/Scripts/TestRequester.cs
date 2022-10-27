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
        SpreadsheetAPI.SendCreateJsonModel(_dataAsset, _sheetIndex);
    }
}
