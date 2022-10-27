using UnityEngine;
using UnityEngine.Networking;

namespace Spreadsheet.API
{
    public interface ISpreadsheetAPI
    {
        void IsDoneCallback(DownloadHandler handler);
    }
}
