using Foundation;

namespace Uniq;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override Microsoft.Maui.Hosting.MauiApp CreateMauiApp() => CommonMauiApp.CreateMauiApp();
}
