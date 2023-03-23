@echo off
SET DIR_0=F:\home\vmdrot\EPAM\Projs\EMK-ATRP\JIRA\UnitTests\Runners\0
SET DIR_1=F:\home\vmdrot\EPAM\Projs\EMK-ATRP\JIRA\UnitTests\Runners\1

SET CURR_FNAME=ZBD.sln.BV.Inspection.Tests.nuc.TestResult.xml
CALL CompareTest_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%"
ECHO %CURR_FNAME%:%ERRORLEVEL%

SET CURR_FNAME=zbd.sln.ZBD.Authorization.Tests.nuc.TestResult.xml
CALL CompareTest_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" "%CURR_FNAME%"
ECHO %CURR_FNAME%:%ERRORLEVEL%

SET CURR_FNAME=zbd.sln.ZBD.Core.FunctionalTests.nuc.TestResult.xml
CALL CompareTest_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" "%CURR_FNAME%"
ECHO %CURR_FNAME%:%ERRORLEVEL%

SET CURR_FNAME=zbd.sln.ZBD.Core.Tests.nuc.TestResult.xml
CALL CompareTest_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" "%CURR_FNAME%"
ECHO %CURR_FNAME%:%ERRORLEVEL%

SET CURR_FNAME=zbd.sln.ZBD.DataAnnotationsValidation.Tests.nuc.TestResult.xml
CALL CompareTest_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" "%CURR_FNAME%"
ECHO %CURR_FNAME%:%ERRORLEVEL%

SET CURR_FNAME=zbd.sln.ZBD.FunctionalTests.nuc.TestResult.xml
CALL CompareTest_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" "%CURR_FNAME%"
ECHO %CURR_FNAME%:%ERRORLEVEL%

SET CURR_FNAME=zbd.sln.ZBD.Infrastructure.Tests.nuc.TestResult.xml
CALL CompareTest_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" "%CURR_FNAME%"
ECHO %CURR_FNAME%:%ERRORLEVEL%

SET CURR_FNAME=zbd.sln.ZBD.IntegrationTests.nuc.TestResult.xml
CALL CompareTest_worker.cmd "%DIR_0%\%CURR_FNAME%" "%DIR_1%\%CURR_FNAME%" "%CURR_FNAME%"
ECHO %CURR_FNAME%:%ERRORLEVEL%