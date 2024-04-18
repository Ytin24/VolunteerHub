using System;
using System.Collections.Generic;

namespace VolunteerHub.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }

    public DateTime? ReportDate { get; set; }

    public string? Description { get; set; }

    public string? Attachment { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
