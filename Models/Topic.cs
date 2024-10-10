using System;
using System.Collections.Generic;

namespace group4.Models;

public class Topic
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int CourseId { get; set; }

    public Course? Course { get; set; }

}
