using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerHub.Models;

namespace VolunteerHub.ViewModels
{
    public class ProjectTileViewModel : ReactiveObject
    {
        public Project Project { get; set; }
    }
}
