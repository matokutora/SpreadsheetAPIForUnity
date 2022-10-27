using System.Text;
using System.IO;

namespace Spreadsheet.IO
{
    public class SpreadSheetIO
    {
        static readonly string FilePath = "Assets/SpreadSheetAPI/IOData/";

        public static void CreateJsonFile(string fileName, string[] dataArray)
        {
            string path = FilePath + fileName + ".txt";
            File.Create(path);

            StreamWriter writer = new StreamWriter(path, false);
            StringBuilder builder = new StringBuilder();
            builder.Append("namespace Spreadsheet.Data.Json \n");
            builder.Append("{ \n");
            builder.Append("    [System.Serializable] \n");
            builder.Append("    public class ModelData \n");
            builder.Append("    { \n");
            for (int index = 0; index < dataArray.Length; index++)
            {
                builder.Append($"        {dataArray[index]}; \n");
            }
            builder.Append("    } \n");
            builder.Append("} \n");

            writer.WriteLine(builder.ToString());
            writer.Flush();
            writer.Close();

            File.Move(path, path + ".json");
        }
    }

}