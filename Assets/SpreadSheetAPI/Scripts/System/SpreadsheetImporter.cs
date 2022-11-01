using System.Collections;
using System.Text;
using UnityEngine.Networking;
using Spreadsheet.API;
using Spreadsheet.Data;
using System;

namespace Spreadsheet
{
    /// <summary>
    /// スプレッドシートからデータを取得するクラス
    /// </summary>
    public class SpreadsheetImporter
    {
        Action<DownloadHandler> _callback;
        StringBuilder _uri;
        ISpreadsheetImport _import;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="api">リクエスト対象者</param>
        /// <param name="deproyKey">macroのデプロイキー</param>
        public SpreadsheetImporter(ISpreadsheetImport api, string deproyKey)
        {
            _uri = new StringBuilder();
            _uri.Append(SpreadsheetAPI.HTTPS);
            _uri.Append($"{deproyKey}/exec");

            _import = api;
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

        public void AddCallbackEvent(Action<DownloadHandler> callback)
        {
            _callback += callback;
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

            _import.IsDoneCallback(request.downloadHandler);
            _callback?.Invoke(request.downloadHandler);
        }
    }
}