using System;
using System.Collections.Generic;
using System.Linq;

namespace VolunteerHub.Models;

public partial class Project {
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<ProjectParticipation> ProjectParticipations { get; set; } = new List<ProjectParticipation>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<ProjectStatus> Statuses { get; set; } = new List<ProjectStatus>();

    public string Status {
        get
        {
            var a = Statuses.LastOrDefault();
            if (a is null) return "неизвестен";
            else return a.StatusName;
        }

    }
}
