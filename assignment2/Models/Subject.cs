using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Models;

public class Subject{
  
  public int? Id;
  public string? Name { get; set; }
  public string? Description { get; set; }
  public int? teacherId { get; set; }
  public List<Student>? studentsEnrolled {get; set;}

  public Subject(int Id, string Name, string Description, int teacherId){
    List<Student> studentsEnrolled = [];
  }
}