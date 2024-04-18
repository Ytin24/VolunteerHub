using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using System;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using VolunteerHub.Models;
using VolunteerHub.Db;
using System.Linq;
using VolunteerHub.Views;
using VolunteerHub.Models.Services;

namespace VolunteerHub.ViewModels;

public class AuthViewModel : ViewModelBase {
    IClassicDesktopStyleApplicationLifetime _desktop = (IClassicDesktopStyleApplicationLifetime)App.Current.ApplicationLifetime;
    private VolunteerDbContext db = new VolunteerDbContext();

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ReactiveCommand<Unit, Unit> LoginAccount { get; set; }
    public UserService _userService { get; private set; }
    public AuthViewModel(IScreen screen, UserService us,
        AdminHubViewModel admin,
        VolunteerHubViewModel volunteer) : base(screen) {
        _userService = us;
        LoginAccount = ReactiveCommand.Create(() => {
            var user = db.Users.FirstOrDefault(x => x.Email == Login && x.PasswordHash == Password);
            if(user == null) {
                return;
            }
            _userService.SetUpUser(user);
            if(_userService.GetUser().RoleId == 1) {
                this.HostScreen.Router.NavigateAndReset.Execute(admin);

            }
            else {
                this.HostScreen.Router.NavigateAndReset.Execute(volunteer);
            }
        });
    }
    public async Task<int> LoginAccountTask() {
        //if (!string.IsNullOrWhiteSpace(Login) && Login[0] == '8') {
        //    Login = $"+7{Login[1..]}";
        //}

        // var box = MessageBoxManager
        //.GetMessageBoxStandard(
        //     "Очипка", ,
        //    ButtonEnum.Ok, Icon.None,
        //    Avalonia.Controls.WindowStartupLocation.CenterOwner
        //    );
        return 1;
        // var result = await box.ShowAsPopupAsync(_desktop.MainWindow);
    }

}
