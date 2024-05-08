using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using VolunteerHub.ViewModels;
using VolunteerHub.Views;

namespace VolunteerHub;

public partial class App : Application {
    public override void Initialize() {

        AvaloniaXamlLoader.Load(this);
        Bootstrapper.Register(Splat.Locator.CurrentMutable, Splat.Locator.Current);

    }

    public override void OnFrameworkInitializationCompleted() {
        var DataContext = GetRequiredService<MainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow() {
                DataContext = DataContext
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
            singleViewPlatform.MainView = new MainView {
                DataContext = DataContext
            };
        }
        DataContext.Router.NavigateAndReset.Execute(GetRequiredService<AuthViewModel>());
        base.OnFrameworkInitializationCompleted();

    }
    private static T GetRequiredService<T>() => Splat.Locator.Current.GetRequiredService<T>();
}
