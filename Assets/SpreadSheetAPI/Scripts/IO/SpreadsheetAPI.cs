using UnityEngine.Networking;
using Spreadsheet.Data;
using Spreadsheet.IO;

namespace Spreadsheet.API
{
    public class SpreadsheetAPI : ISpreadsheetImport
    {
        static public readonly string ResourceFilePath = "SpreadsheetAPI/";
        static public readonly string HTTPS = "https://script.google.com/macros/s/";

        SpreadSheetIO _io = new SpreadSheetIO();

        static SpreadsheetAPI s_instance = null;
        static protected SpreadsheetAPI Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new SpreadsheetAPI();
                }

                return s_instance;
            }
        }

        #if UNITY_EDITOR

        protected void OnGetCreateJsonModel(DownloadHandler handler, string fileName)
        {
            string[] keyArray = handler.text.Split(",");
            _io.CreateJsonDataModel(fileName, keyArray);
        }

        protected void OnGetGetJsonModel(DownloadHandler handler, string fileName)
        {
            _io.CreateJsonFile(fileName, handler.text);
        }

        /// <summary>
        /// JsonFileの作成リクエスト
        /// </summary>
        /// <param name="dataAsset">作成するAsset</param>
        /// <param name="sheetIndex">Sheet番号</param>
        public static void SendCreateJsonDataModel(SpreadsheetDataAsset dataAsset, int sheetIndex, string fileName)
        {
            SpreadsheetImporter requester = new SpreadsheetImporter(Instance, dataAsset.DeproyID);

            string spreadSheetID = dataAsset.SpreadSheetID;
            string sheetID = dataAsset.SheetIDArray[sheetIndex];
            SpreadsheetQueryData queryData = new SpreadsheetQueryData(spreadSheetID, sheetID);

            requester.SetQuery(queryData, SpreadsheetQueryType.CreateJsonModel);
            requester.AddCallbackEvent(handler => Instance.OnGetCreateJsonModel(handler, fileName));
            requester.Request();
        }

        public static void SendCreateJsonFile(SpreadsheetDataAsset dataAsset, int sheetIndex, string fileName)
        {
            SpreadsheetImporter requester = new SpreadsheetImporter(Instance, dataAsset.DeproyID);

            string spreadSheetID = dataAsset.SpreadSheetID;
            string sheetID = dataAsset.SheetIDArray[sheetIndex];
            SpreadsheetQueryData queryData = new SpreadsheetQueryData(spreadSheetID, sheetID);

            requester.SetQuery(queryData, SpreadsheetQueryType.GetJsonModel);
            requester.AddCallbackEvent(handler => Instance.OnGetGetJsonModel(handler, fileName));
            requester.Request();
        }

        /// <summary>
        /// JsonFileの作成
        /// </summary>
        /// <param name="handler">Json形式のデータ</param>
        void ISpreadsheetImport.IsDoneCallback(DownloadHandler handler)
        {
            UnityEngine.Debug.Log("IsDone");
        }

        #endif
    }
}
