//set DOTNET_CLI_NO_JIT=true

using Microsoft.EntityFrameworkCore;
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
            try
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

                builder.Services.AddDbContext<SQLiteContext>();

                var db = new SQLiteContext();
                db.Database.EnsureCreated();
                
                db.Dispose();

                builder.Services.AddMemoryCache();

                builder.Services.AddSingleton<IConnectionService, ConnectionService>();

                builder.Services.AddSingleton<IDbService, DbService>();
                builder.Services.AddSingleton<ITabService, TabService>();




#if DEBUG
                builder.Services.AddBlazorWebViewDeveloperTools();
                builder.Logging.AddDebug();
#endif

                return builder.Build();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}
