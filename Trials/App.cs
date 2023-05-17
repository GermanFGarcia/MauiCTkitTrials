using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

namespace Uniq;

internal static class Trial
{
    public enum Types { Bindings, Gestures }

    public static readonly Types Type = Types.Gestures;
}

/// <summary>
/// Common class used by all platform implementations.
/// </summary>
public static class CommonMauiApp
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            // create the common app
            .UseMauiApp<App>()
            // initialize the .NET MAUI Community Toolkit 
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            // use any resource as fonts, controls, ...
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            // register layers
            .RegisterAppLayers();

        return builder.Build();
    }

    internal static MauiAppBuilder RegisterAppLayers(this MauiAppBuilder builder)
    {
        var services = builder.Services;

        switch (Trial.Type)
        {
            case Trial.Types.Bindings:
                services.AddSingleton<BindingsModel>();
                services.AddSingleton<BindingsNexus>();
                services.AddSingleton<BindingsView>();
                break;

            case Trial.Types.Gestures:
                services.AddSingleton<GesturesModel>();
                services.AddSingleton<GesturesNexus>();
                services.AddSingleton<GesturesView>();
                break;
        }

        return builder;
    }
}

internal class App : Application 
{ 
    public App(IServiceProvider services)
	{
        switch (Trial.Type)
        {
            case Trial.Types.Bindings:
                {
                    var model = services.GetService<BindingsModel>();
                    var nexus = services.GetService<BindingsNexus>();
                    var view = services.GetService<BindingsView>();
                    MainPage = view;
                }
                break;

            case Trial.Types.Gestures:
                {
                    var model = services.GetService<GesturesModel>();
                    var nexus = services.GetService<GesturesNexus>();
                    var view = services.GetService<GesturesView>();
                    MainPage = view;
                }
                break;    
        }
    }
}

