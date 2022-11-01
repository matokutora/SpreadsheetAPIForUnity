using UnityEngine.Networking;
using Spreadsheet.Data;

namespace Spreadsheet.API
{
    public interface ISpreadsheetImport
    {
        void IsDoneCallback(DownloadHandler handler);
    }

    public interface ISpreadsheetWrite
    {
        void SetWrite(out SpreadsheetWriteData data);
    }
}
