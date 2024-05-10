using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using VolunteerHub.Db;
using VolunteerHub.Models;
using VolunteerHub.Models.Services;

namespace VolunteerHub.ViewModels {
    public class RegistrationViewModel : ViewModelBase {
        public ReactiveCommand<Unit, Unit> CreateAccount { get; set; }
        private readonly VolunteerDbContext db = new VolunteerDbContext();
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        private bool _showError = false;
        public bool ShowError { get => _showError; set { _showError = value; this.RaisePropertyChanged(); } }
        public RegistrationViewModel(IScreen screen, 
            UserService us,
            VolunteerHubViewModel volunteer) : base(screen) {
            CreateAccount = ReactiveCommand.Create(() => {
                ShowError = false;
                var user = new Models.User() {
                    Email = Email,
                    Username = Login,
                    PasswordHash = Password,
                    Role = db.UserRoles.First(x => x.RoleName == "volunteer")
                };
                db.Users.Add(user);
                try {
                    db.SaveChanges();
                }
                catch (DbUpdateException) {
                    db.Users.Remove(user);
                    ShowError = true;
                    return;
                }
                us.SetUpUser(user);
                HostScreen.Router.NavigateAndReset.Execute(volunteer);
            });
        }

    }
}
