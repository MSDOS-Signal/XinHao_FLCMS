using ZXing.Net.Maui;

namespace ERPWMS.Client.NativePages;

public partial class ScanPage : ContentPage
{
    private TaskCompletionSource<string> _tcs;
    private bool _isScanning = true;

    public ScanPage(TaskCompletionSource<string> tcs)
    {
        InitializeComponent();
        _tcs = tcs;

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All, // 支持二维码和条形码
            AutoRotate = true,
            Multiple = false
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        cameraBarcodeReaderView.IsDetecting = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        cameraBarcodeReaderView.IsDetecting = false;
    }

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (!_isScanning) return;

        var first = e.Results?.FirstOrDefault();
        if (first != null && !string.IsNullOrEmpty(first.Value))
        {
            _isScanning = false;
            
            // 必须在 UI 线程操作
            Dispatcher.Dispatch(async () =>
            {
                // 震动反馈
                try
                {
                    HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                }
                catch { }

                await Navigation.PopModalAsync();
                _tcs.TrySetResult(first.Value);
            });
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
        _tcs.TrySetResult(string.Empty);
    }
}