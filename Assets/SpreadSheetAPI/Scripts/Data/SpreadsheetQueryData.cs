using System.Text;

namespace Spreadsheet.Data
{
    public enum SpreadsheetQueryType
    {
        CreateJsonModel,
        GetJsonModel,
    }

    /// <summary>
    /// スプレッドシートに対するクエリのデータ
    /// </summary>
    public struct SpreadsheetQueryData
    {
        string _spreadSheetID;
        string _sheetID;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="spreadSheetID">スプレッドシートのID</param>
        /// <param name="sheetID">スプレッドシート内に対するシートのID</param>
        public SpreadsheetQueryData(string spreadSheetID, string sheetID)
        {
            _spreadSheetID = spreadSheetID;
            _sheetID = sheetID;
        }

        /// <summary>
        /// クエリを作成
        /// </summary>
        /// <returns></returns>
        public string CreateQuery()
        {
            StringBuilder query = new StringBuilder();

            query.Append($"&spreadsheetID={_spreadSheetID}");
            query.Append($"&sheetID={_sheetID}");

            return query.ToString();
        }
    }

}