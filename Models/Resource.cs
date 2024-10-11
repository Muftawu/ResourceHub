using System;
using System.Collections.Generic;

namespace group4.Models;

public class Resource
{
    public int Id { get; set; }

    public required string Name { get; set; } 

    public string? Comments { get; set; } 

    public string? FilePath { get; set; }

    public string? UserId { get; set; }

    public int CourseId { get; set; }

    public int TopicId { get; set; }

    public User? User { get; set; }

    public Course? Course { get; set; }

    public Topic? Topic { get; set; }

}
