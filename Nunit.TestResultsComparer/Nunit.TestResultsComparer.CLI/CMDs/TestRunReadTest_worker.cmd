@echo off
SET XML_PATH=%~1

..\bin\Debug\Nunit.TestResultsComparer.CLI.exe ReadTest "%XML_PATH%" %2 %3 %4 %5 %6 %7 %8 %9 1> TestRunReadTest.cmd.log 2>&1
EXIT /B %ERRORLEVEL%