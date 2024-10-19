using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using System.Globalization;

namespace Divisas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            CultureInfo cultureInfo = new CultureInfo("es-MX");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var builder = MauiApp.CreateBuilder();
            builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
            //.UseMauiCommunityToolkit()
            //.UseMauiCommunityToolkitCore()
            //.UseMauiCommunityToolkitMarkup()
            //.ConfigureEssentials();

            // Borrar la base de datos existente
            //ConexionDB.BorrarBaseDeDatos("Divisas.db");
            //Console.WriteLine("Base de datos recreada.");

            using (var dbContext = new DivisasDbContext())
            {
                dbContext.Database.EnsureCreated();
                Console.WriteLine("Base de datos creada o ya existía.");
                dbContext.Dispose();
            }

            Microsoft.Maui.Handlers.ToolbarHandler.Mapper.AppendToMapping("CustomNavigationView", (handler, view) =>
            {
        #if ANDROID
        handler.PlatformView.ContentInsetStartWithNavigation = 0;
        handler.PlatformView.SetContentInsetsAbsolute(0, 0);
        #endif
            });

        #if DEBUG
            builder.Logging.AddDebug();
        #endif

            return builder.Build();
        }
    }
}
