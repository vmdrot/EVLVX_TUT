@echo off
SET ALLURE_RESULTS_FOLDER_PATH=%~1

..\bin\Debug\Nunit.TestResultsComparer.CLI.exe ReadAllureFolder "%ALLURE_RESULTS_FOLDER_PATH%" %2 %3 %4 %5 %6 %7 %8 %9 1> "ReadAllureFolder.cmd.log" 2> "ReadAllureFolder.cmd.err.log" 
EXIT /B %ERRORLEVEL%