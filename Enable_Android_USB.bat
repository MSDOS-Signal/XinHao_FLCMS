@echo off
REM Android USB Debugging Setup
echo --------------------------------------------------------
echo Android USB Debugging Connection Setup
echo Mapping Android localhost:8080 - PC localhost:8080
echo --------------------------------------------------------

REM Define ADB Path
set "ADB_PATH=D:\DDD\Android\android-sdk\platform-tools\adb.exe"

REM Check if path exists
if exist "%ADB_PATH%" goto FoundADB
set "ADB_PATH=adb"
:FoundADB

echo Using ADB: %ADB_PATH%

echo [1/4] Resetting ADB server...
taskkill /F /IM adb.exe >nul 2>&1
"%ADB_PATH%" kill-server >nul 2>&1

echo [2/4] Starting ADB server...
"%ADB_PATH%" start-server

echo [3/4] Checking connected devices...
"%ADB_PATH%" devices

echo [4/4] Running adb reverse...
"%ADB_PATH%" reverse tcp:8080 tcp:8080

if errorlevel 1 goto Error

:Success
echo --------------------------------------------------------
echo [SUCCESS] Connection established!
echo Now your Android phone can access PC WebAPI via http://localhost:8080
goto End

:Error
echo --------------------------------------------------------
echo [ERROR] Failed to setup connection.
echo Troubleshooting:
echo 1. Ensure USB Debugging is ON.
echo 2. Check for Allow USB Debugging prompt on phone.
echo 3. Replug USB cable.

:End
pause
