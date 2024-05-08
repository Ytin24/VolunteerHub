using System.Collections.Generic;

namespace VolunteerHub.Models;

public partial class User {
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<ProjectParticipation> ProjectParticipations { get; set; } = new List<ProjectParticipation>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual UserRole Role { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
