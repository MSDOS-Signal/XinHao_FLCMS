using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace ERPWMS.Client;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseBarcodeReader()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

        // Register HttpClient
        builder.Services.AddScoped(sp => 
        {
            string baseAddress = "http://localhost:8080";
            
            // Android 特殊处理
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // 模拟器使用 10.0.2.2
                if (DeviceInfo.DeviceType == DeviceType.Virtual)
                {
                    baseAddress = "http://10.0.2.2:8080";
                }
                // 真机需配合 adb reverse tcp:8080 tcp:8080 使用 localhost
                else
                {
                    baseAddress = "http://localhost:8080";
                }
            }

            return new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        });

        builder.Services.AddScoped<Services.InventoryService>();
        builder.Services.AddScoped<Services.OrderService>();
        builder.Services.AddScoped<Services.WorkOrderService>();
        builder.Services.AddScoped<Services.DeviceService>();
        builder.Services.AddScoped<Services.EnterpriseService>();
        builder.Services.AddScoped<Services.IScannerService, Services.ScannerService>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
