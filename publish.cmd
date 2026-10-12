@echo off
rem Sestaví UsbCam do jednoho samostatného exe (self-contained, win-x64) do složky publish\.
setlocal
cd /d "%~dp0"

if exist publish rmdir /s /q publish

dotnet publish UsbCam.csproj -c Release -r win-x64 --self-contained true -o publish ^
  -p:PublishSingleFile=true ^
  -p:IncludeNativeLibrariesForSelfExtract=true ^
  -p:EnableCompressionInSingleFile=true ^
  -p:DebugType=none
if errorlevel 1 (
  echo.
  echo Publish selhal.
  exit /b 1
)

copy /y "%~dp0readme.md" "%~dp0publish\readme.md" >nul

echo.
echo Hotovo: %~dp0publish\UsbCam.exe
endlocal
