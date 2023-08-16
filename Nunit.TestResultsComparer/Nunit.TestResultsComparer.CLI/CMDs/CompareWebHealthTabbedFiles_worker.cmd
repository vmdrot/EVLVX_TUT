@echo off
SET PATH1=%~1
SET PATH2=%~2
SET KEY_COL=%~3
SET RES_COL=%~4
SET LOG_FILE_PFX=%~5

..\bin\Debug\Nunit.TestResultsComparer.CLI.exe CompareWebHealthTabbedFiles "%PATH1%" "%PATH2%" "%KEY_COL%" "%RES_COL%" %6 %7 %8 %9 1> "CompareWebHealthTabbedFiles.cmd.%LOG_FILE_PFX%.log" 2>&1
EXIT /B %ERRORLEVEL%