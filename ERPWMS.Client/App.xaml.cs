namespace ERPWMS.Client;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var navPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Colors.Black,
            BarTextColor = Colors.White
        };
        // 隐藏主页面的原生导航栏，让 Blazor 接管 UI
        NavigationPage.SetHasNavigationBar(navPage.CurrentPage, false);
        
		return new Window(navPage) { Title = "炘灏全链路核心管理系统" };
	}
}
