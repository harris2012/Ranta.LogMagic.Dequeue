pushd D:\ServiceRoot\DequeueLogService

@echo off
set /p var=是否要卸载 WCF服务(Y/N):
if "%var%" == "y" (goto uninstall) else (goto batexit)

:uninstall
SET PATH=%PATH%;C:\Windows\Microsoft.NET\Framework\v4.0.30319

call InstallUtil.exe /u Ranta.LogMagic.Dequeue.Server.exe

pause

:batexit
exit