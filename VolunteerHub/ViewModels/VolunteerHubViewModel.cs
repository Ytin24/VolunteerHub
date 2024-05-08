using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
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
            Projects = new VolunteerDbContext().Projects.Include(s => s.Statuses).ToList();
        }
        public List<Project> Projects { get; set; }


    }
}
