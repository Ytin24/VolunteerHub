﻿using Avalonia.Controls.ApplicationLifetimes;
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

namespace VolunteerHub.ViewModels;

public class AuthViewModel : ViewModelBase {
    IClassicDesktopStyleApplicationLifetime _desktop = (IClassicDesktopStyleApplicationLifetime)App.Current.ApplicationLifetime;
    private VolunteerDbContext db = new VolunteerDbContext();

    public static User Me { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ReactiveCommand<Unit, Unit> LoginAccount { get; set; }
    public AuthViewModel(IScreen screen) : base(screen) {
        LoginAccount = ReactiveCommand.Create(() => {
            var user = db.Users.FirstOrDefault(x => x.Email == Login && x.PasswordHash == Password);
            if(user == null) {
                return;
            }
            Me = user;
            if(Me.RoleId == 1) {
                this.HostScreen.Router.NavigateAndReset.Execute(new AdminHubViewModel(this));

            }
            else {
                this.HostScreen.Router.NavigateAndReset.Execute(new VolunteerHubViewModel(this));
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