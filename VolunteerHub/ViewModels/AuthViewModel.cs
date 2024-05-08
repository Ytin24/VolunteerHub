using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using VolunteerHub.Db;
using VolunteerHub.Models.Services;

namespace VolunteerHub.ViewModels;

public class AuthViewModel : ViewModelBase {
    private readonly IClassicDesktopStyleApplicationLifetime _desktop = (IClassicDesktopStyleApplicationLifetime)App.Current.ApplicationLifetime;
    private readonly VolunteerDbContext db = new VolunteerDbContext();

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ReactiveCommand<Unit, Unit> LoginAccount { get; set; }
    public UserService _userService { get; private set; }
    public AuthViewModel(IScreen screen, UserService us,
        AdminHubViewModel admin,
        VolunteerHubViewModel volunteer) : base(screen) {
        _userService = us;
        LoginAccount = ReactiveCommand.Create(() => {
            var user = db.Users.Include(x => x.Role).FirstOrDefault(x => x.Email == Login && x.PasswordHash == Password);
            if (user == null) {
                return;
            }
            _userService.SetUpUser(user);
            if (_userService.GetUser().Role.RoleName == "admin") {
                HostScreen.Router.NavigateAndReset.Execute(admin);

            }
            else {
                HostScreen.Router.NavigateAndReset.Execute(volunteer);
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
