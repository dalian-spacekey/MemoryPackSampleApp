using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SampleApp.Mobile.Models;
using SampleApp.Mobile.ViewModels;
using SampleApp.Mobile.Views;
using SampleApp.Shared;
using MemoryPackClient = SampleApp.Mobile.Models.MemoryPackClient;

namespace SampleApp.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MemoryPackClient>();
        builder.Services.AddSingleton<MessageModel>();
        builder.Services.AddSingleton<MessageListPageViewModel>();
        builder.Services.AddSingleton<MessageDetailPageViewModel>();
        builder.Services.AddSingleton<PersonModel>();
        builder.Services.AddSingleton<PersonListPageViewModel>();
        builder.Services.AddSingleton<PersonDetailPageViewModel>();

        builder.Services.AddTransient<MessageListPage>();
        builder.Services.AddTransient<MessageDetailPage>();
        builder.Services.AddTransient<PersonListPage>();
        builder.Services.AddTransient<PersonDetailPage>();

        Routing.RegisterRoute("MessageDetailPage", typeof(MessageDetailPage));
        Routing.RegisterRoute("PersonDetailPage", typeof(PersonDetailPage));

        return builder.Build();
	}
}

public interface INavigationService
{
    Task InitializeAsync();
    Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);
    Task PopAsync();
}

//public class NavigationService : INavigationService
//{
//    public Task InitializeAsync()
//    {
//        return NavigateToAsync(string.IsNullOrEmpty(_settingsService.AuthAccessToken)
//            ? "//Login"
//            : "//Main/Catalog";
//    }

//    public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
//    {
//        return
//            routeParameters != null
//                ? Shell.Current.GoToAsync(route, routeParameters)
//                : Shell.Current.GoToAsync(route);
//    }

//    public Task PopAsync()
//    {
//        throw new NotImplementedException();
//    }
//}