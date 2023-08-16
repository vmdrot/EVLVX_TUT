@echo off
SET DIR_0=F:\home\vmdrot\EPAM\Projs\EMK-ATRP\JIRA\EMCEE-2116\verify\clean
SET DIR_1=F:\home\vmdrot\EPAM\Projs\EMK-ATRP\JIRA\EMCEE-2116\verify\Ninject.3.2.2

SET LAST_ERR=0
SET CURR_FNAME=Check_WebAPIs.txt
CALL CompareWebHealthTabbedFiles_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" API_KEY Result "" -Debug -SkipLogo
SET LAST_ERR=%ERRORLEVEL%
ECHO %CURR_FNAME%:%LAST_ERR%
EXIT /B %LAST_ERR%

SET CURR_FNAME=Check_WebApps.txt
CALL CompareWebHealthTabbedFiles_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" Url HttpStatusCode
SET LAST_ERR=%ERRORLEVEL%
ECHO %CURR_FNAME%:%LAST_ERR%
