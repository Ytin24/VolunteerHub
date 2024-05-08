using System.Collections.Generic;

namespace VolunteerHub.Models;

public partial class Task {
    public int TaskId { get; set; }

    public int ProjectId { get; set; }

    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }

    public int? AssignedTo { get; set; }

    public string Status { get; set; } = null!;

    public virtual User? AssignedToNavigation { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
