using System.Text;

namespace Spreadsheet.Data
{
    public enum SpreadsheetQueryType
    {
        CreateJsonModel,
        GetJsonModel,
    }

    /// <summary>
    /// �X�v���b�h�V�[�g�ɑ΂���N�G���̃f�[�^
    /// </summary>
    public struct SpreadsheetQueryData
    {
        string _spreadSheetID;
        string _sheetID;

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="spreadSheetID">�X�v���b�h�V�[�g��ID</param>
        /// <param name="sheetID">�X�v���b�h�V�[�g���ɑ΂���V�[�g��ID</param>
        public SpreadsheetQueryData(string spreadSheetID, string sheetID)
        {
            _spreadSheetID = spreadSheetID;
            _sheetID = sheetID;
        }

        /// <summary>
        /// �N�G�����쐬
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