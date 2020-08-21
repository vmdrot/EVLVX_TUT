@echo off
REM ..\bin\Debug\VSUsagesInsight.CLI.exe GetLatestStablePackage DotNetOpenAuth.Ultimate 1> GetLatestStablePackage.cmd.log 2>&1
..\bin\Debug\VSUsagesInsight.CLI.exe GetLatestStablePackage Autofac 1>> GetLatestStablePackage.cmd.log 2>>&1
EXIT /B 0.
..\bin\Debug\VSUsagesInsight.CLI.exe GetLatestStablePackage EntityFramework 1>> GetLatestStablePackage.cmd.log 2>>&1
..\bin\Debug\VSUsagesInsight.CLI.exe GetLatestStablePackage LumenWorksCsvReader 1>> GetLatestStablePackage.cmd.log 2>>&1
..\bin\Debug\VSUsagesInsight.CLI.exe GetLatestStablePackage Microsoft.AspNet.WebApi.Client 1>> GetLatestStablePackage.cmd.log 2>>&1