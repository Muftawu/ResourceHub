using System;
using System.Collections.Generic;

namespace group4.Models;

public partial class Resource
{
    public string ResourceId { get; set; } = null!;

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public string Topic { get; set; } = null!;

    public string Name { get; set; } = null!;

    public byte[] Resource1 { get; set; } = null!;

    public string? Comment { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
