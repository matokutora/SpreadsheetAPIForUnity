using UnityEngine.Networking;
using Spreadsheet.Data;
using Spreadsheet.IO;

namespace Spreadsheet.API
{
    public class SpreadsheetAPI : ISpreadsheetAPI
    {
        protected SpreadsheetQueryType QueryType { get; set; }

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

        protected void OnGetCreateJsonModel(DownloadHandler handler)
        {
            string[] keyArray = handler.text.Split(",");
            SpreadSheetIO.CreateJsonDataModel("Sample", keyArray);
        }

        protected void OnGetGetJsonModel(DownloadHandler handler)
        {
            SpreadSheetIO.CreateJsonFile("Sample", handler.text);
        }

        /// <summary>
        /// JsonFileの作成リクエスト
        /// </summary>
        /// <param name="dataAsset">作成するAsset</param>
        /// <param name="sheetIndex">Sheet番号</param>
        public static void SendCreateJsonDataModel(SpreadsheetDataAsset dataAsset, int sheetIndex)
        {
            SpreadsheetImporter requester = new SpreadsheetImporter(Instance, dataAsset.DeproyDey);

            string spreadSheetID = dataAsset.SpreadSheetID;
            string sheetID = dataAsset.SheetIDArray[sheetIndex];
            SpreadsheetQueryData queryData = new SpreadsheetQueryData(spreadSheetID, sheetID);

            requester.SetQuery(queryData, SpreadsheetQueryType.CreateJsonModel);
            requester.AddCallbackEvent(Instance.OnGetCreateJsonModel);
            requester.Request();
        }

        public static void SendCreateJsonFile(SpreadsheetDataAsset dataAsset, int sheetIndex)
        {
            SpreadsheetImporter requester = new SpreadsheetImporter(Instance, dataAsset.DeproyDey);

            string spreadSheetID = dataAsset.SpreadSheetID;
            string sheetID = dataAsset.SheetIDArray[sheetIndex];
            SpreadsheetQueryData queryData = new SpreadsheetQueryData(spreadSheetID, sheetID);

            requester.SetQuery(queryData, SpreadsheetQueryType.GetJsonModel);
            requester.AddCallbackEvent(Instance.OnGetGetJsonModel);
            requester.Request();
        }

        /// <summary>
        /// JsonFileの作成
        /// </summary>
        /// <param name="handler">Json形式のデータ</param>
        void ISpreadsheetAPI.IsDoneCallback(DownloadHandler handler)
        {
            
        }
    }
}
