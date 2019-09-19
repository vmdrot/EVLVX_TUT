SET URL=%~1
SET SAVE_DIR=%~2

..\bin\Debug\Evolvex.VKUtil.exe ReadAndDownloadZaycevNet "%URL%" "%SAVE_DIR%" > "%SAVE_DIR%\vkutil.log" 