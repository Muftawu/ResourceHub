using System;
using System.Collections.Generic;

namespace group4.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepName { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
