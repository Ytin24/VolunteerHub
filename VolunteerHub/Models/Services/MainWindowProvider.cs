using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace VolunteerHub.Models.Services {
    public class MainWindowProvider : IMainWindowProvider {
        public Window GetMainWindow() {
            var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;

            return lifetime.MainWindow;
        }
    }
    public interface IMainWindowProvider {
        Window GetMainWindow();
    }
}
