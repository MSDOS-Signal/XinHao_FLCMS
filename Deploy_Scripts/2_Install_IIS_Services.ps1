# Windows Server 2025 部署脚本 - 步骤 2: 安装服务与IIS站点
# 必须以管理员身份运行

$PublishPath = "C:\inetpub\ERPWMS_MES"
$AppPoolName = "ERPWMS_Pool"
$ApiSiteName = "ERPWMS.WebAPI"
$UiSiteName = "ERPWMS.BlazorUI"

# 1. 检查并安装 IIS (如果未安装)
Write-Host "检查 IIS 状态..." -ForegroundColor Yellow
$iis = Get-WindowsFeature Web-Server
if ($iis.InstallState -ne "Installed") {
    Write-Host "正在安装 IIS..." -ForegroundColor Cyan
    Install-WindowsFeature -Name Web-Server -IncludeManagementTools
}

# 提示安装 .NET 8 Hosting Bundle
Write-Host "请确保已安装 .NET 8 Hosting Bundle！" -ForegroundColor Red
Write-Host "下载地址: https://dotnet.microsoft.com/en-us/download/dotnet/8.0" -ForegroundColor Red
Start-Sleep -Seconds 3

# 2. 配置应用程序池
if (!(Get-WebAppPoolState -Name $AppPoolName -ErrorAction SilentlyContinue)) {
    Write-Host "创建应用程序池: $AppPoolName" -ForegroundColor Cyan
    New-WebAppPool -Name $AppPoolName
    Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name "managedRuntimeVersion" -Value "" # No Managed Code for Core
    Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name "processModel.identityType" -Value "LocalSystem" # 简化权限
}

# 3. 创建 WebAPI 站点 (端口 8080)
if (Get-Website -Name $ApiSiteName -ErrorAction SilentlyContinue) {
    Remove-Website -Name $ApiSiteName
}
Write-Host "创建站点 $ApiSiteName (Port 8080)..." -ForegroundColor Cyan
New-Website -Name $ApiSiteName -Port 8080 -PhysicalPath "$PublishPath\WebAPI" -ApplicationPool $AppPoolName

# 4. 创建 BlazorUI 站点 (端口 8082)
if (Get-Website -Name $UiSiteName -ErrorAction SilentlyContinue) {
    Remove-Website -Name $UiSiteName
}
Write-Host "创建站点 $UiSiteName (Port 8082)..." -ForegroundColor Cyan
New-Website -Name $UiSiteName -Port 8082 -PhysicalPath "$PublishPath\BlazorUI" -ApplicationPool $AppPoolName

# 5. 安装 Windows Services
Write-Host "安装后台服务..." -ForegroundColor Cyan

# WCS Service
sc.exe stop "ERPWMS.WCS"
sc.exe delete "ERPWMS.WCS"
sc.exe create "ERPWMS.WCS" binPath= "$PublishPath\Services\WCS\ERPWMS.WCS.Service.exe" start= auto displayname= "ERPWMS WCS Device Service"
sc.exe start "ERPWMS.WCS"

# SCADA Service
sc.exe stop "ERPWMS.SCADA"
sc.exe delete "ERPWMS.SCADA"
sc.exe create "ERPWMS.SCADA" binPath= "$PublishPath\Services\SCADA\ERPWMS.SCADA.Service.exe" start= auto displayname= "ERPWMS SCADA Monitor Service"
sc.exe start "ERPWMS.SCADA"

# Job Service
sc.exe stop "ERPWMS.Job"
sc.exe delete "ERPWMS.Job"
sc.exe create "ERPWMS.Job" binPath= "$PublishPath\Services\Job\ERPWMS.Job.Service.exe" start= auto displayname= "ERPWMS Hangfire Job Service"
sc.exe start "ERPWMS.Job"

Write-Host "部署全部完成！" -ForegroundColor Green
Write-Host "API 地址: http://localhost:8080/swagger"
Write-Host "UI 地址: http://localhost:8082"
