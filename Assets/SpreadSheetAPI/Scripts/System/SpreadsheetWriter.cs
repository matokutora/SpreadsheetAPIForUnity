using System;
using System.Text;
using System.Collections;
using UnityEngine.Networking;
using Spreadsheet.API;
using Spreadsheet.Data;

namespace Spreadsheet
{
    public class SpreadsheetWriter
    {
        ISpreadsheetWrite _write;

        public SpreadsheetWriter(ISpreadsheetWrite write)
        {
            _write = write;
        }

        public void Request(SpreadsheetDataAsset asset, int sheetIndex)
        {
            string uri = CreateQuery(asset, sheetIndex);
            UnityWebRequest request = UnityWebRequest.Get(uri);

            IEnumerator enumerator = SendWeb(request);

            while (enumerator.MoveNext()) { }
        }

        string CreateQuery(SpreadsheetDataAsset asset, int sheetIndex)
        {
            StringBuilder builder = new StringBuilder();

            _write.SetWrite(out SpreadsheetWriteData data);

            builder.Append(SpreadsheetAPI.HTTPS);
            builder.Append($"{asset.DeproyID}/exec");
            builder.Append($"?write=IsWrite");
            builder.Append($"&spreadsheetID={asset.SpreadSheetID}");
            builder.Append($"&sheetID={asset.SheetIDArray[sheetIndex]}");
            builder.Append($"&column={data.Column}");
            builder.Append($"&row={data.Row}");
            builder.Append($"&writeValue={data.Value}");

            return builder.ToString();
        }

        IEnumerator SendWeb(UnityWebRequest request)
        {
            yield return request.SendWebRequest();
            while (!request.isDone) { yield return 0; }
        }
    }
}
