using Spreadsheet.API;
using Spreadsheet.Data.Json;
using UnityEngine;

namespace Spreadsheet
{
    public class SpreadsheetOwner
    {
        public T Request<T>(SpreadsheetJsonType jsonType)
        {
            string path = SpreadsheetAPI.ResourceFilePath + "Json/" + jsonType.ToString();
            TextAsset a = Resources.Load<TextAsset>(path);

            T model = JsonUtility.FromJson<T>(a.text);
            return model;
        }
    }
}


