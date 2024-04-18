using Avalonia.Controls;
using ReactiveUI;
using System.Linq;
using VolunteerHub.Db;
using VolunteerHub.Models;
using VolunteerHub.ViewModels;

namespace VolunteerHub.Views.Controls {
    public partial class ProjectTile : UserControl {
        private VolunteerDbContext db = new VolunteerDbContext();

        public ProjectTile() {
            InitializeComponent();
            this.DataContextChanged += ProjectTile_DataContextChanged;
        }

        private void ProjectTile_DataContextChanged(object? sender, System.EventArgs e) {
            if (this.DataContext is null) return;
            else if(this.DataContext is AdminProject) {
                this.DataContext = (this.DataContext as AdminProject).Project;
            }
            if(AuthViewModel.Me.RoleId == 1) {
                Register.IsVisible = false;
                Unregister.IsVisible = false;
                return;
            }
            var registered = db.ProjectParticipations.Any(x => x.ProjectId == (this.DataContext as Project).ProjectId && x.UserId == AuthViewModel.Me.UserId);
            Register.IsVisible = !registered;
            Unregister.IsVisible = registered;
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
            db.ProjectParticipations.Add(new() {
                ParticipationDate = System.DateTime.Now,
                ProjectId = (this.DataContext as Project).ProjectId,
                UserId = AuthViewModel.Me.UserId
            });
            db.SaveChanges();
            Register.IsVisible = false;
            Unregister.IsVisible = true;

        }
        private void ButtonUnreg_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
            var proj = db.ProjectParticipations.FirstOrDefault(x => x.ProjectId == (this.DataContext as Project).ProjectId && x.UserId == AuthViewModel.Me.UserId);
            if (proj == null) { return; }
            db.ProjectParticipations.Remove(proj);
            db.SaveChanges();
            Register.IsVisible = true;
            Unregister.IsVisible = false;
        }
    }
}
