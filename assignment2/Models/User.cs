using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Models;

public abstract class User
{
  public string? Name { get; }
  public string? Username { get; }
  public string? Role { get; }
  public string? Password { get; }

  public User(string? name, string? username, string? password, string? role)
  {
    Name = name;
    Username = username;
    Password = password;
    Role = role;
  }
}