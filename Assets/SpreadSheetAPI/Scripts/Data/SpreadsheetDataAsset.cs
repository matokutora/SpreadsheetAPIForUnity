using UnityEngine;

namespace Spreadsheet.Data
{
    /// <summary>
    /// スプレッドシートからデータを取得するためのアセット
    /// </summary>
    [CreateAssetMenu(fileName = "SpreadsheetData_[DataName]")]
    public class SpreadsheetDataAsset : ScriptableObject
    {
        [SerializeField] string _deproyKey;
        [SerializeField] string _spreadSheetID;
        [SerializeField] string[] _sheetIDArray;

        /// <summary>
        /// デプロイキー
        /// </summary>
        public string DeproyDey => _deproyKey;
        /// <summary>
        /// スプレッドシートのID
        /// </summary>
        public string SpreadSheetID => _spreadSheetID;
        /// <summary>
        /// スプレッドシート内に対するシートのID配列
        /// </summary>
        public string[] SheetIDArray => _sheetIDArray;
    }
}
