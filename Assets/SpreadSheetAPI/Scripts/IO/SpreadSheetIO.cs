#if UNITY_EDITOR

using System.Linq;
using System.IO;

namespace Spreadsheet.IO
{
    public class SpreadSheetIO
    {
        readonly string FilePath = "Assets/SpreadSheetAPI/Data/";
        readonly string JsonFilePath = "Assets/Resources/SpreadsheetAPI/Json/";

        public void CreateJsonDataModel(string fileName, string[] dataArray)
        {
            string path = fileName + ".txt";
            StreamWriter writer = File.CreateText(FilePath + "Model/" + path);

            FileWriter fileWriter = new FileWriter();
            fileWriter.WriteDataModel(writer, fileName, dataArray);
            writer.Flush();
            writer.Close();
            writer.Dispose();
            
            string newPath = FilePath + "Model/" + path.Replace(".txt", "DataModel.cs");
            
            File.Move(FilePath + "Model/" + path, newPath);
        }

        public void CreateJsonFile(string fileName, string json)
        {
            string path = fileName + ".txt";
            StreamWriter writer = File.CreateText(JsonFilePath + path);

            writer.WriteLine(json);
            writer.Flush();
            writer.Close();
            writer.Dispose();

            string newPath = JsonFilePath + path.Replace(".txt", ".json");

            File.Move(JsonFilePath + path, newPath);
        }

        public void CreateJsonEnum()
        {
            string[] fileNameArray = 
                Directory.GetFiles(JsonFilePath, "*", SearchOption.TopDirectoryOnly)
                .Select(file => file = Path.GetFileNameWithoutExtension(file))
                .Where(file => !file.Contains(".json"))
                .ToArray();

            string path = FilePath + "Json/" + "JsonEnum.txt";
            StreamWriter writer = new StreamWriter(path, false);

            FileWriter fileWriter = new FileWriter();
            fileWriter.WriteJsonEnum(writer, fileNameArray);

            writer.Flush();
            writer.Close();
            writer.Dispose();

            File.Copy(path, FilePath + "Json/" + "SpreadsheetJsonType.cs");
        }
    }
}
#endif