@echo off
SET XML_PATH1=%~1
SET XML_PATH2=%~2
SET LOG_FILE_PFX=%~3

..\bin\Debug\Nunit.TestResultsComparer.CLI.exe CompareTest "%XML_PATH1%" "%XML_PATH1%" %3 %4 %5 %6 %7 %8 %9 1> "CompareTest.cmd.%LOG_FILE_PFX%.log" 2>&1
EXIT /B %ERRORLEVEL%