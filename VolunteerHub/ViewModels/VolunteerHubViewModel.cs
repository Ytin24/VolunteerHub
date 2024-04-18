using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerHub.Db;
using VolunteerHub.Models;
using VolunteerHub.Models.Services;

namespace VolunteerHub.ViewModels {
    public class VolunteerHubViewModel : ViewModelBase {
        //public VolunteerHubViewModel( UserData user, IRoutableViewModel Caller) : base(Caller.HostScreen) {

        //}
        public UserService _userService { get; set; }

        public VolunteerHubViewModel(IScreen screen, UserService us) : base(screen) {
            _userService = us;
            Projects = new VolunteerDbContext().Projects.ToList();
        }
        public List<Project> Projects { get; set; }

        
    }
}
