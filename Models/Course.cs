using System;
using System.Collections.Generic;

namespace group4.Models;

public class Course
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }

}
