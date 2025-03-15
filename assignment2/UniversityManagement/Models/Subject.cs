using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityManagement.Models;

public class Subject
{

  public Guid? Id;
  public string? Name { get; set; }
  public string? Description { get; set; }
  public Guid? TeacherId { get; set; }
  public List<Guid>? StudentsEnrolled { get; set; } = [];

  public Subject(string name, string description, Guid teacherId)
  {
    Id = Guid.NewGuid();
    this.Name = name;
    this.Description = description;
    this.TeacherId = teacherId;

  }
}