using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using System.Diagnostics;
using System.Linq;
using VolunteerHub.Db;
using VolunteerHub.Models;
using VolunteerHub.Models.Services;
using VolunteerHub.ViewModels;

namespace VolunteerHub.Views.Controls
{
    public partial class ProjectTile : UserControl
    {
        private VolunteerDbContext db = new VolunteerDbContext();

        public ProjectTile()
        {
            InitializeComponent();
            this.DataContextChanged += ProjectTile_DataContextChanged;

        }

        private void ProjectTile_DataContextChanged(object? sender, System.EventArgs e) {
            if (this.DataContext is null)
            {
                this.IsVisible = false;
                return;
            }
            else
            {
                this.IsVisible = true;
            }
            if (UserService.SGetUser().RoleId == 1) {
                Register.IsVisible = false;
                Unregister.IsVisible = false;
                return;
            }
            var registered = db.ProjectParticipations.Any(x => x.ProjectId == (this.DataContext as Project).ProjectId && x.UserId == UserService.SGetUser().UserId);
            if (UserService.SGetUser().Role.RoleName == "admin")
            {
                Register.IsVisible = false;
                Unregister.IsVisible = false;
                return;
            }
            Register.IsVisible = !registered;
            Unregister.IsVisible = registered;
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
            db.ProjectParticipations.Add(new() {
                ParticipationDate = System.DateTime.Now,
                ProjectId = (this.DataContext as Project).ProjectId,
                UserId = UserService.SGetUser().UserId
            });
            db.SaveChanges();
            Register.IsVisible = false;
            Unregister.IsVisible = true;

        }
        private void ButtonUnreg_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
            var proj = db.ProjectParticipations.FirstOrDefault(x => x.ProjectId == (this.DataContext as Project).ProjectId && x.UserId == UserService.SGetUser().UserId);
            if (proj == null) { return; }
            db.ProjectParticipations.Remove(proj);
            db.SaveChanges();
            Register.IsVisible = true;
            Unregister.IsVisible = false;
        }

    }
}
