# Windows Server 2025 部署脚本 - 步骤 1: 发布
# 请在管理员权限的 PowerShell 中运行

$SolutionPath = "E:\C#app\ERPWMS_MES_SuperProject"
$PublishPath = "C:\inetpub\ERPWMS_MES"

# 1. 清理旧发布
Write-Host "正在清理旧文件..." -ForegroundColor Yellow
if (Test-Path $PublishPath) {
    Remove-Item -Path $PublishPath -Recurse -Force
}
New-Item -ItemType Directory -Path "$PublishPath\WebAPI" -Force
New-Item -ItemType Directory -Path "$PublishPath\BlazorUI"
New-Item -ItemType Directory -Path "$PublishPath\Services\WCS"
New-Item -ItemType Directory -Path "$PublishPath\Services\SCADA"
New-Item -ItemType Directory -Path "$PublishPath\Services\Job"

# 2. 发布 WebAPI
Write-Host "正在发布 WebAPI..." -ForegroundColor Cyan
dotnet publish "$SolutionPath\ERPWMS.WebAPI\ERPWMS.WebAPI.csproj" -c Release -o "$PublishPath\WebAPI"

# 3. 发布 BlazorUI
Write-Host "正在发布 BlazorUI..." -ForegroundColor Cyan
dotnet publish "$SolutionPath\ERPWMS.BlazorUI\ERPWMS.BlazorUI.csproj" -c Release -o "$PublishPath\BlazorUI"

# 4. 发布 Worker Services
Write-Host "正在发布 Worker Services..." -ForegroundColor Cyan
dotnet publish "$SolutionPath\ERPWMS.WCS.Service\ERPWMS.WCS.Service.csproj" -c Release -o "$PublishPath\Services\WCS"
dotnet publish "$SolutionPath\ERPWMS.SCADA.Service\ERPWMS.SCADA.Service.csproj" -c Release -o "$PublishPath\Services\SCADA"
dotnet publish "$SolutionPath\ERPWMS.Job.Service\ERPWMS.Job.Service.csproj" -c Release -o "$PublishPath\Services\Job"

Write-Host "发布完成！文件已生成至 $PublishPath" -ForegroundColor Green
Write-Host "请继续运行 2_Install_IIS_Services.ps1 进行安装" -ForegroundColor Yellow
