using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Spreadsheet.Data;
using Spreadsheet.API;
using Spreadsheet.IO;

namespace Spreadsheet.Edit
{
    public class SpreadsheetAPIWindow : EditorWindow
    {
        readonly Vector2 WindowSize = new Vector2(500, 250);

        [MenuItem("Window/SpreadsheetAPI")]
        static void OpenWindow()
        {
            EditorWindow window = GetWindow<SpreadsheetAPIWindow>();
            window.titleContent = new GUIContent(nameof(SpreadsheetAPIWindow));
        }

        void OnEnable()
        {
            string path = SpreadsheetAPI.ResourceFilePath + "UXML/";
            VisualTreeAsset asset = Resources.Load<VisualTreeAsset>(path + "SpreadsheetAPIUXML");
            asset.CloneTree(rootVisualElement);

            StyleSheet style = Resources.Load<StyleSheet>(path + "SpreadsheetStyle");
            rootVisualElement.styleSheets.Add(style);

            Button modelButton = rootVisualElement.Q<Button>("GenerateRequestModel");
            modelButton.clickable.clicked += () => GenerateModel();
            
            Button jsonButton = rootVisualElement.Q<Button>("GenerateRequestJson");
            jsonButton.clickable.clicked += () => GenerateJson();

            Button enumButton = rootVisualElement.Q<Button>("GenerateJsonEnum");
            enumButton.clickable.clicked += () => GenerateJsonEnum();
        }

        void OnGUI()
        {
            minSize = WindowSize;
            maxSize = WindowSize;
        }

        void GenerateModel()
        {
            ObjectField field = rootVisualElement.Q<ObjectField>("DataAsset");
            int sheetIndex = rootVisualElement.Q<IntegerField>("SheetIndex").value;
            string fileName = rootVisualElement.Q<TextField>("ModelFile").value;
            
            SpreadsheetDataAsset dataAsset = field.value as SpreadsheetDataAsset;

            if (field != null && fileName != "")
            {
                SpreadsheetAPI.SendCreateJsonDataModel(dataAsset, sheetIndex, fileName);
            }
        }

        void GenerateJson()
        {
            ObjectField field = rootVisualElement.Q<ObjectField>("DataAsset");
            int sheetIndex = rootVisualElement.Q<IntegerField>("SheetIndex").value;
            string fileName = rootVisualElement.Q<TextField>("JsonFile").value;
            
            SpreadsheetDataAsset dataAsset = field.value as SpreadsheetDataAsset;

            if (field != null && fileName != "")
            {
                SpreadsheetAPI.SendCreateJsonFile(dataAsset, sheetIndex, fileName);
            }
        }

        void GenerateJsonEnum()
        {
            SpreadSheetIO io = new SpreadSheetIO();
            io.CreateJsonEnum();
        }
    }
}