using System.Collections;
using System.Text;
using UnityEngine.Networking;
using Spreadsheet.API;
using Spreadsheet.Data;

namespace Spreadsheet
{
    /// <summary>
    /// スプレッドシートからデータを取得するクラス
    /// </summary>
    public class SpreadsheetImporter
    {
        StringBuilder _uri;
        ISpreadsheetAPI _api;

        readonly string HTTPS = "https://script.google.com/macros/s/";

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="api">リクエスト対象者</param>
        /// <param name="deproyKey">macroのデプロイキー</param>
        public SpreadsheetImporter(ISpreadsheetAPI api, string deproyKey)
        {
            _uri = new StringBuilder();
            _uri.Append(HTTPS);
            _uri.Append($"{deproyKey}/exec");

            _api = api;
        }

        /// <summary>
        /// クエリの追加
        /// </summary>
        /// <param name="queryData">クエリデータ</param>
        public void SetQuery(SpreadsheetQueryData queryData, SpreadsheetQueryType queryType)
        {
            _uri.Append($"?queryType={queryType}");
            _uri.Append(queryData.CreateQuery());
        }

        /// <summary>
        /// Web上に申請
        /// </summary>
        public void Request()
        {
            UnityWebRequest request = UnityWebRequest.Get(_uri.ToString());

            IEnumerator enumerator = SendWeb(request);

            while (enumerator.MoveNext()) { }
        }

        IEnumerator SendWeb(UnityWebRequest request)
        {
            yield return request.SendWebRequest();
            while (!request.isDone) { yield return 0; }

            _api.IsDoneCallback(request.downloadHandler);
        }
    }
}