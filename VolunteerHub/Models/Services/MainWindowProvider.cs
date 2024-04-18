using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using ReactiveUI;

namespace VolunteerHub.Models.Services
{
    public class MainWindowProvider : IMainWindowProvider
    {
        public Window GetMainWindow()
        {
            var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;

            return lifetime.MainWindow;
        }
    }
    public interface IMainWindowProvider
    {
        Window GetMainWindow();
    }
}
