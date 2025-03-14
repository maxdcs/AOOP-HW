using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace assignment2.Models;

public class Student : User
{

  private List<Subject> enrolledSubjects;

  public Student(string? name, string? username, string? password, string? role): base(name, username, password, role)
  {
    role = "Student";
    enrolledSubjects = [];
  }
}
