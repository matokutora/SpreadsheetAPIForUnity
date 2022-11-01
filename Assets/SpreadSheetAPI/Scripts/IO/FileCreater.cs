#if UNITY_EDITOR

using System.IO;
using System.Text;

namespace Spreadsheet.IO
{
    public struct FileWriter
    {
        public void WriteDataModel(StreamWriter writer, string path, string[] dataArray)
        {
            string className = path + "DataModel";

            StringBuilder builder = new StringBuilder();
            builder.Append("namespace Spreadsheet.Data.Model \n");
            builder.Append("{ \n");
            builder.Append("    [System.Serializable] \n");
            builder.Append($"    public class {className} \n");
            builder.Append("    { \n");
            builder.Append("        public Model[] Data;\n");
            builder.Append("        [System.Serializable]\n");
            builder.Append("        public class Model\n");
            builder.Append("        {\n");
            for (int index = 0; index < dataArray.Length; index++)
            {
                if (dataArray[index] != "")
                {
                    builder.Append($"           public string {dataArray[index]}; \n");
                }
            }
            builder.Append("        }\n");
            builder.Append("    } \n");
            builder.Append("} \n");
            writer.WriteLine(builder.ToString());
        }

        public void WriteJsonEnum(StreamWriter writer, string[] fileNameArray)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("namespace Spreadsheet.Data.Json\n");
            builder.Append("{\n");
            builder.Append("    public enum SpreadsheetJsonType\n");
            builder.Append("    {\n");
            for (int index = 0; index < fileNameArray.Length; index++)
            {
                builder.Append($"        {fileNameArray[index]},\n");
            }
            builder.Append("    }\n");
            builder.Append("}\n");

            writer.WriteLine(builder.ToString());
        }
    }
}

#endif