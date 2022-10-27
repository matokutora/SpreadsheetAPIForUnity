using UnityEngine.Networking;
using Spreadsheet.Data;
using Spreadsheet.IO;

namespace Spreadsheet.API
{
    public class SpreadsheetAPI : ISpreadsheetAPI
    {
        SpreadSheetIO _spreadSheetIO = new SpreadSheetIO();

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

        /// <summary>
        /// JsonFile�̍쐬���N�G�X�g
        /// </summary>
        /// <param name="dataAsset">�쐬����Asset</param>
        /// <param name="sheetIndex">Sheet�ԍ�</param>
        public static void SendCreateJsonModel(SpreadsheetDataAsset dataAsset, int sheetIndex)
        {
            SpreadsheetImporter requester = new SpreadsheetImporter(Instance, dataAsset.DeproyDey);

            string spreadSheetID = dataAsset.SpreadSheetID;
            string sheetID = dataAsset.SheetIDArray[sheetIndex];
            SpreadsheetQueryData queryData = new SpreadsheetQueryData(spreadSheetID, sheetID);

            requester.SetQuery(queryData, SpreadsheetQueryType.CreateJsonModel);
            requester.Request();
        }

        /// <summary>
        /// JsonFile�̍쐬
        /// </summary>
        /// <param name="handler">Json�`���̃f�[�^</param>
        void ISpreadsheetAPI.IsDoneCallback(DownloadHandler handler)
        {
            string[] keyArray = handler.text.Split(",");
            SpreadSheetIO.CreateJsonFile("Sample", keyArray);
        }
    }
}
