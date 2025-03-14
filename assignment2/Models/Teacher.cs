using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Models;

public class Teacher : User
{
    public List<Subject> createdSubjects =[];

    public Teacher(string? name, string? username, string? password, string? role): base(name, username, password, role){
        role = "Teacher";
    }
}