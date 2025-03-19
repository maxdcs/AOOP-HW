using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityManagement.Models;

public class Subject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid TeacherId { get; set; }
    public List<Guid> StudentsEnrolled { get; set; } = new List<Guid>();

    public Subject(string name, string description, Guid teacherId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        TeacherId = teacherId;
        StudentsEnrolled = new List<Guid>();
    }

    public Subject() { }
}