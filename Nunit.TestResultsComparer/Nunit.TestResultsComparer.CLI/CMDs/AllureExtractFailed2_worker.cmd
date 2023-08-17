@echo off
SET ALLURE_RESULTS_FOLDER_PATH=%~1

..\bin\Debug\Nunit.TestResultsComparer.CLI.exe AllureExtractFailed2 "%ALLURE_RESULTS_FOLDER_PATH%" %2 %3 %4 %5 %6 %7 %8 %9 1> "AllureExtractFailed2.cmd.log" 2>&1
EXIT /B %ERRORLEVEL%