ws = WScript.CreateObject('WScript.Shell');
ln = ws.SpecialFolders('Startup') + '\\' + 'RotController.lnk';
sc = ws.CreateShortcut(ln);
sc.TargetPath = ws.CurrentDirectory + '\\RotController.exe';
sc.Save();
