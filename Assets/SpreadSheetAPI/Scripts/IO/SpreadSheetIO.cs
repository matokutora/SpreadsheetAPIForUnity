using System.IO;

namespace Spreadsheet.IO
{
    public class SpreadSheetIO
    {
        static readonly string FilePath = "Assets/SpreadSheetAPI/IOData/";

        public static void CreateJsonDataModel(string fileName, string[] dataArray)
        {
            string path = fileName + ".txt";
            StreamWriter writer = File.CreateText(FilePath + path);

            FileWriter fileWriter = new FileWriter();
            fileWriter.WriteDataModel(writer, fileName, dataArray);
            writer.Flush();
            writer.Close();
            writer.Dispose();
            
            string newPath = FilePath + "Model/" + path.Replace(".txt", "DataModel.cs");
            
            File.Move(FilePath + path, newPath);
        }

        public static void CreateJsonFile(string fileName, string json)
        {
            string path = fileName + ".txt";
            StreamWriter writer = File.CreateText(FilePath + path);

            writer.WriteLine(json);
            writer.Flush();
            writer.Close();
            writer.Dispose();

            string newPath = FilePath + "Json/" + path.Replace(".txt", ".json");

            File.Move(FilePath + path, newPath);
        }
    }

}