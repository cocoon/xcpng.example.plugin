set ProjectName=%1
set TargetDir=%2
set TargetDirNoQuotes=%TargetDir:"=%
set DevEnvDir=%3
set DevEnvDirNoQuotes=%DevEnvDir:"=%

CALL "%DevEnvDirNoQuotes%..\Tools\VsDevCmd.bat"

for /f %%i in ('where resgen.exe') do set RESGEN=%%i
for /f %%i in ('where al.exe') do set AL=%%i

"%RESGEN%" "%TargetDir%%ProjectName%.resx"
"%AL%" /t:lib /embed:"%TargetDir%%ProjectName%.resources" /out:"%TargetDir%%ProjectName%.resources.dll"