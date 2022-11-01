using System.Collections;
using System.Text;
using UnityEngine.Networking;
using Spreadsheet.API;
using Spreadsheet.Data;
using System;

namespace Spreadsheet
{
    /// <summary>
    /// �X�v���b�h�V�[�g����f�[�^���擾����N���X
    /// </summary>
    public class SpreadsheetImporter
    {
        Action<DownloadHandler> _callback;
        StringBuilder _uri;
        ISpreadsheetAPI _api;

        readonly string HTTPS = "https://script.google.com/macros/s/";

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="api">���N�G�X�g�Ώێ�</param>
        /// <param name="deproyKey">macro�̃f�v���C�L�[</param>
        public SpreadsheetImporter(ISpreadsheetAPI api, string deproyKey)
        {
            _uri = new StringBuilder();
            _uri.Append(HTTPS);
            _uri.Append($"{deproyKey}/exec");

            _api = api;
        }

        /// <summary>
        /// �N�G���̒ǉ�
        /// </summary>
        /// <param name="queryData">�N�G���f�[�^</param>
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
        /// Web��ɐ\��
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
            _callback?.Invoke(request.downloadHandler);
        }
    }
}