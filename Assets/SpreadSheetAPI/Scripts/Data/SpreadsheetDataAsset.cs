using UnityEngine;

namespace Spreadsheet.Data
{
    /// <summary>
    /// �X�v���b�h�V�[�g����f�[�^���擾���邽�߂̃A�Z�b�g
    /// </summary>
    [CreateAssetMenu(fileName = "SpreadsheetData_[DataName]")]
    public class SpreadsheetDataAsset : ScriptableObject
    {
        [SerializeField] string _deproyID;
        [SerializeField] string _spreadSheetID;
        [SerializeField] string[] _sheetIDArray;

        /// <summary>
        /// �f�v���C�L�[
        /// </summary>
        public string DeproyID => _deproyID;
        /// <summary>
        /// �X�v���b�h�V�[�g��ID
        /// </summary>
        public string SpreadSheetID => _spreadSheetID;
        /// <summary>
        /// �X�v���b�h�V�[�g���ɑ΂���V�[�g��ID�z��
        /// </summary>
        public string[] SheetIDArray => _sheetIDArray;
    }
}
