using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityManagement.Models;

public abstract class User
{
  public Guid Id { get; protected set; }
  public string? Name { get; }
  public string? Username { get; }
  public string? Role { get; }
  public string? Password { get; }

  public User(string? name, string? username, string? password, string? role)
  {
    Id = Guid.NewGuid();
    Name = name;
    Username = username;
    Password = password;
    Role = role;
  }
}