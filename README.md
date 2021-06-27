# FolderListener
It is a Windows Console Service (based on Topshelf https://github.com/Topshelf/Topshelf) that listens to changes in the folder and showing changes on the screen. It can helps logging changes in some windows-folder.
Also, you can use it as an example of working with FileSystemEventHandler class and how to host the .net service with TopShelf.
___
Service parameters set in App.config file:
* FileRules config section used for setting parameters of file types for listening changes. 
* Folders config section used for setlist of folders for listening. 
___
DefaultRule tag contains destination settings for moving files.
___

Example of FolderListener settings(code bit from App.Config):
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
