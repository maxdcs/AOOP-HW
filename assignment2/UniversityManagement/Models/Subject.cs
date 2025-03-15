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
  public Guid? teacherId { get; set; }
  public List<Guid>? studentsEnrolled { get; set; } = [];

  public Subject(string Name, string Description, Guid teacherId)
  {
    Id = Guid.NewGuid();
  }
}