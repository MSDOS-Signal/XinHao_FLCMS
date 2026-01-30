# Package Script

$BuildDir = "Builds"
$WinDir = "$BuildDir\Windows"
$AndroidDir = "$BuildDir\Android"
$LinuxDir = "$BuildDir\Linux"

Write-Host "Cleaning build directory..." -ForegroundColor Yellow
if (Test-Path $BuildDir) { Remove-Item $BuildDir -Recurse -Force }
New-Item -ItemType Directory -Path $WinDir | Out-Null
New-Item -ItemType Directory -Path $AndroidDir | Out-Null
New-Item -ItemType Directory -Path $LinuxDir | Out-Null

# 1. Windows Package
Write-Host "Packaging Windows Version..." -ForegroundColor Cyan
# Publish Unpackaged (Let dotnet infer RID)
dotnet publish ERPWMS.Client/ERPWMS.Client.csproj -f net9.0-windows10.0.19041.0 -c Release -p:WindowsPackageType=None -p:WindowsAppSDKSelfContained=true --self-contained -o $WinDir
if (Test-Path "$WinDir\ERPWMS.Client.exe") {
    Rename-Item "$WinDir\ERPWMS.Client.exe" "$WinDir\Xinhao_System.exe"
}

# 2. Android Package
Write-Host "Building Android Version..." -ForegroundColor Cyan
dotnet build ERPWMS.Client/ERPWMS.Client.csproj -f net9.0-android -c Release
$ApkPath = Get-ChildItem "ERPWMS.Client/bin/Release/net9.0-android" -Recurse -Filter "*.apk" | Select-Object -First 1
if ($ApkPath) {
    Copy-Item $ApkPath.FullName "$AndroidDir\Xinhao_System.apk"
    Write-Host "Android APK copied to: $AndroidDir\Xinhao_System.apk" -ForegroundColor Green
} else {
    Write-Host "Android APK not found." -ForegroundColor Red
}

# 3. Linux Package
Write-Host "Packaging Linux Version (WebAPI and BlazorUI)..." -ForegroundColor Cyan
dotnet publish ERPWMS.WebAPI/ERPWMS.WebAPI.csproj -c Release -r linux-x64 --self-contained -o "$LinuxDir\WebAPI"
dotnet publish ERPWMS.BlazorUI/ERPWMS.BlazorUI.csproj -c Release -r linux-x64 --self-contained -o "$LinuxDir\BlazorUI"

Write-Host "------------------------------------------------"
Write-Host "Packaging Complete! Output in 'Builds' folder." -ForegroundColor Green
Write-Host "Windows: $WinDir"
Write-Host "Android: $AndroidDir"
Write-Host "Linux: $LinuxDir"
