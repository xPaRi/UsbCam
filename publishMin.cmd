@echo off
rem Sestaví UsbCam do jednoho malého exe BEZ .NET (framework-dependent, win-x64) do složky publishMin\.
rem Na cílovém počítači musí být nainstalován .NET 10 Desktop Runtime (x64).
setlocal
cd /d "%~dp0"

if exist publishMin rmdir /s /q publishMin

dotnet publish UsbCam.csproj -c Release -r win-x64 --self-contained false -o publishMin ^
  -p:PublishSingleFile=true ^
  -p:DebugType=none
if errorlevel 1 (
  echo.
  echo Publish selhal.
  exit /b 1
)

copy /y "%~dp0readme.md" "%~dp0publishMin\readme.md" >nul

echo.
echo Hotovo: %~dp0publishMin\UsbCam.exe
echo Vyžaduje .NET 10 Desktop Runtime (x64).
endlocal
