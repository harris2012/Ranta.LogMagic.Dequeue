pushd D:\ServiceRoot\DequeueLogService

@echo off
set /p var=是否要安装 WCF 服务(Y/N):
if "%var%" == "y" (goto install) else (goto batexit)

:install
SET PATH=%PATH%;C:\Windows\Microsoft.NET\Framework\v4.0.30319

call InstallUtil.exe Ranta.LogMagic.Dequeue.Server.exe

pause

:batexit
exit