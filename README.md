# FolderListener
Служба, которая слушает изменения в папке и выводит их на экран.
___
Параметры службы задаются в App.config.
Это секция для типов файлов FileRules и Секция для списка папок для прослушивания Folders. 
___
DefaultRule содержит destination Для перемещения файлов, которые подошли под текущие настройки в секции FileRules.
___

Пример настроек:
```
<FileWatcherSettings>
    <Folders>
      <Folder name="temp" path="C:\temp\" />
      <Folder name="temp2" path="C:\temp2\" />
      <Folder name="temp3" path="C:\temp3\" />
    </Folders>
    <DefaultRule destination="C:\result\" isNeededNumber = "true" isNeededDate ="true"/>
    <CurrentCulture name="Ru-ru"/>
    <FilesRules>
      <FileRule name ="txt" pattern ="\w+\.txt" destination ="C:\resultTxt\" isNeededNumber = "true" isNeededDate ="true"/>
      <FileRule name = "cs" pattern ="\w+\.cs" destination ="C:\resultSharp\" isNeededNumber = "false" isNeededDate ="true"/>
      <FileRule name ="jpg" pattern ="\w+\.jpg" destination ="C:\resultJpg\" isNeededNumber = "true" isNeededDate ="false"/>
      <FileRule name ="html" pattern ="\w+\.html" destination ="C:\resultHtml\" isNeededNumber = "false" isNeededDate ="false"/>
    </FilesRules>
  </FileWatcherSettings>
```
___
