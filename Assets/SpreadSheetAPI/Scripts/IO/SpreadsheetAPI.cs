using UnityEngine.Networking;
using Spreadsheet.Data;
using Spreadsheet.IO;
using Spreadsheet.Data.Json;
using UnityEngine;

namespace Spreadsheet.API
{
    public class SpreadsheetAPI : ISpreadsheetAPI
    {
        static public readonly string ResourceFilePath = "SpreadsheetAPI/";

        SpreadSheetIO _io = new SpreadSheetIO();

        static SpreadsheetAPI s_instance = null;
        static public SpreadsheetAPI Instance
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

        public T Request<T>(SpreadsheetJsonType jsonType)
        {
            string path = ResourceFilePath + "Json/" + jsonType.ToString();
            TextAsset a = Resources.Load<TextAsset>(path);

            T model = JsonUtility.FromJson<T>(a.text);
            return model;
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
        /// JsonFile�̍쐬���N�G�X�g
        /// </summary>
        /// <param name="dataAsset">�쐬����Asset</param>
        /// <param name="sheetIndex">Sheet�ԍ�</param>
        public static void SendCreateJsonDataModel(SpreadsheetDataAsset dataAsset, int sheetIndex, string fileName)
        {
            SpreadsheetImporter requester = new SpreadsheetImporter(Instance, dataAsset.DeproyDey);

            string spreadSheetID = dataAsset.SpreadSheetID;
            string sheetID = dataAsset.SheetIDArray[sheetIndex];
            SpreadsheetQueryData queryData = new SpreadsheetQueryData(spreadSheetID, sheetID);

            requester.SetQuery(queryData, SpreadsheetQueryType.CreateJsonModel);
            requester.AddCallbackEvent(handler => Instance.OnGetCreateJsonModel(handler, fileName));
            requester.Request();
        }

        public static void SendCreateJsonFile(SpreadsheetDataAsset dataAsset, int sheetIndex, string fileName)
        {
            SpreadsheetImporter requester = new SpreadsheetImporter(Instance, dataAsset.DeproyDey);

            string spreadSheetID = dataAsset.SpreadSheetID;
            string sheetID = dataAsset.SheetIDArray[sheetIndex];
            SpreadsheetQueryData queryData = new SpreadsheetQueryData(spreadSheetID, sheetID);

            requester.SetQuery(queryData, SpreadsheetQueryType.GetJsonModel);
            requester.AddCallbackEvent(handler => Instance.OnGetGetJsonModel(handler, fileName));
            requester.Request();
        }

        /// <summary>
        /// JsonFile�̍쐬
        /// </summary>
        /// <param name="handler">Json�`���̃f�[�^</param>
        void ISpreadsheetAPI.IsDoneCallback(DownloadHandler handler)
        {
            UnityEngine.Debug.Log("IsDone");
        }

        #endif
    }
}
