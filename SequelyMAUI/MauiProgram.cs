//set DOTNET_CLI_NO_JIT=true

using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MudBlazor.Services;
using SequelyMAUI.Interfaces;
using SequelyMAUI.Services;

namespace SequelyMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<IConnectionHandler, ConnectionHandler>();

//#if WINDOWS
//            builder.ConfigureLifecycleEvents(events => {
//                events.AddWindows(windows => {
//                    windows.OnClosed((window, args) => {
//                        var connectionHandler = MauiApplication.Current.Services.GetService(IConnectionHandler);
//                        connectionHandler.SaveConnections();
                            
//                        }); 
//                    });
//                }); 
//#endif

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
