namespace ERPWMS.Client.Services;

public interface IScannerService
{
    Task<string> ScanAsync();
}

public class ScannerService : IScannerService
{
    public async Task<string> ScanAsync()
    {
        var tcs = new TaskCompletionSource<string>();
        
        // 必须在主线程创建 UI 控件
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            var page = new NativePages.ScanPage(tcs);
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(page);
            }
            else
            {
                tcs.SetResult(string.Empty);
            }
        });

        return await tcs.Task;
    }
}