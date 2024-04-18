using Avalonia.Controls;
using DynamicData.Aggregation;
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
using VolunteerHub.Views.Controls;

namespace VolunteerHub.ViewModels
{
    public class AdminHubViewModel : ViewModelBase
    {
        private VolunteerDbContext db = new VolunteerDbContext();
        public AdminHubViewModel(IRoutableViewModel Caller) : base(Caller.HostScreen) {
            UpdateData();
            RemoveVolunteer = ReactiveCommand.Create(() => {
                var projectParticipations = db.ProjectParticipations.ToList();
                var target = projectParticipations.FirstOrDefault(x => x.ProjectId == SelectedAProject?.Project?.ProjectId && x.UserId == SelectedUser?.UserId);
                if (target == null) return;
                db.ProjectParticipations.Remove(target);
                db.SaveChanges();
                UpdateData();
            });
            SelectedAProject.WhenAnyValue(x => x)
            .Subscribe(_ => UpdateData());
        }

        private void UpdateData() {
            Projects.Clear();
            var projects = db.Projects.Include(p => p.ProjectParticipations).ToList();
            var users = db.Users.ToList();
            foreach (var project in projects) {
                var projectUsers = project.ProjectParticipations.Select(pp => pp.User).ToList();
                Projects.Add(new AdminProject {
                    Project = project,
                    Users = users.Where(u => projectUsers.Any(pu => pu.UserId == u.UserId)).ToList()
                });
            }
        }

        public List<AdminProject> Projects { get; set; } = new List<AdminProject>();
        public AdminProject SelectedAProject { get; set; } = new();
        public User SelectedUser { get; set; }
        public ReactiveCommand<Unit, Unit> RemoveVolunteer { get; set; }

    }
}
