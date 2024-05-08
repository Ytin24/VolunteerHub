using System;

namespace VolunteerHub.Models;

public partial class ProjectParticipation {
    public int ParticipationId { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public DateTime? ParticipationDate { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
