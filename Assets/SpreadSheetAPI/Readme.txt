---Folderに関して---

原則、Folder名を変更しないでください。

Assets/SpreadSheetAPI/Data/Model => 作成されたJsonModelが保存されます。
Assets/Resources/SpreadSheetAPI/Json => SpreadSheetからのデータをJson形式で保存します。

Import時に上記のFolderにプレファブが置いてあります。
削除してください。


---Spreadsheet側---

下記のGASをコピペしてください。


function doGet(param) {

  let spreadSheet = SpreadsheetApp.openById(param.parameter.spreadsheetID);
  let sheet = spreadSheet.getSheetByName(param.parameter.sheetID);

  if(param.parameter.queryType == 'CreateJsonModel') {
      return GetKey(sheet);
    } else {
      return SetJson(sheet);
    }
}

function GetKey(sheet) {

  var columnLength = sheet.getLastColumn();

  var keyArray = '';
  for(let index = 1; index <= columnLength; index++) {
    var value = sheet.getRange(1, index).getValue();
    keyArray += value + ',';
  }
  
  return ContentService.createTextOutput(keyArray);
}

function SetJson(sheet) {

  var rows = sheet.getDataRange().getValues();
  var keys = [];

  //データ開始行を検索する。B列が空欄の場所はコメントや説明行になる。
  //データ開始行は、Jsonのキーになる。これを以後キー列と呼ぶ。
  var index = 0;
  for(index=0; index<rows.length; ++index)
  {
    if(rows[index][1] == "") continue;
    keys = rows[index];
    break;
  };

  //1行目のF列にバージョンが入っているので取得する
  var version = rows[0][5];

  //キー列のあと、データ列だけを切り出す
  //先頭に_がついているキー列のデータは取得しない
  rows.splice(0, index+1);
  var datum = rows.map(function(row) {
    var obj = {};
    row.map(function(item, index) {
      if(String(keys[index]) == "") return;
      if(String(keys[index]).indexOf("_") == 0) return;
      obj[String(keys[index])] = String(item);
    });
    return obj;
  });

  //バージョン情報を付ける感じで成形
  var json = {
    Version: version,
    Data: datum
  };

  //jsonデータを文字列にして返す
  return ContentService.createTextOutput(JSON.stringify(json, null, 2))
  .setMimeType(ContentService.MimeType.JSON);
}