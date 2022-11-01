---Folder�Ɋւ���---

�����AFolder����ύX���Ȃ��ł��������B

Assets/SpreadSheetAPI/Data/Model => �쐬���ꂽJsonModel���ۑ�����܂��B
Assets/Resources/SpreadSheetAPI/Json => SpreadSheet����̃f�[�^��Json�`���ŕۑ����܂��B

Import���ɏ�L��Folder�Ƀv���t�@�u���u���Ă���܂��B
�폜���Ă��������B


---Spreadsheet��---

���L��GAS���R�s�y���Ă��������B


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

  //�f�[�^�J�n�s����������BB�񂪋󗓂̏ꏊ�̓R�����g������s�ɂȂ�B
  //�f�[�^�J�n�s�́AJson�̃L�[�ɂȂ�B������Ȍ�L�[��ƌĂԁB
  var index = 0;
  for(index=0; index<rows.length; ++index)
  {
    if(rows[index][1] == "") continue;
    keys = rows[index];
    break;
  };

  //1�s�ڂ�F��Ƀo�[�W�����������Ă���̂Ŏ擾����
  var version = rows[0][5];

  //�L�[��̂��ƁA�f�[�^�񂾂���؂�o��
  //�擪��_�����Ă���L�[��̃f�[�^�͎擾���Ȃ�
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

  //�o�[�W��������t���銴���Ő��`
  var json = {
    Version: version,
    Data: datum
  };

  //json�f�[�^�𕶎���ɂ��ĕԂ�
  return ContentService.createTextOutput(JSON.stringify(json, null, 2))
  .setMimeType(ContentService.MimeType.JSON);
}