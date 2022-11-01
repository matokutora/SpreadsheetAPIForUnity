namespace Spreadsheet.Data.Model 
{ 
    [System.Serializable] 
    public class SampleDataModel 
    { 
        public Model[] Data;
        [System.Serializable]
        public class Model
        {
           public string AAA; 
           public string BBB; 
           public string CCC; 
        }
    } 
} 

