@echo off
SET IN_FIL_PATH=%~1

..\bin\Debug\Nunit.TestResultsComparer.CLI.exe CompareMultipleRuns "%IN_FIL_PATH%" %2 %3 %4 %5 %6 %7 %8 %9 1> "CompareMultipleRuns.cmd.log" 2>&1
EXIT /B %ERRORLEVEL%