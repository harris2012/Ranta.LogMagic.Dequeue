set PATH=%PATH%;C:\Program Files (x86)\MSBuild\12.0\Bin

msbuild /t:rebuild /p:configuration=debug

del /q D:\ServiceRoot\DequeueLogService

xcopy /y bin\Debug D:\ServiceRoot\DequeueLogService

pause
