using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace UniversityManagement.Models
{
  public class UserManager
  {
    private const string UsersFilePath = "Data/users.json";
    private List<User> userList = [];
    
    public UserManager()
    {
      LoadUsersFromFile();
    }
    
    private void LoadUsersFromFile()
    {
      try
      {
        if (File.Exists(UsersFilePath))
        {
          string jsonString = File.ReadAllText(UsersFilePath);
          var options = new JsonSerializerOptions
          {
            WriteIndented = true,
            Converters = { new UserJsonConverter() }
          };
          var users = JsonSerializer.Deserialize<List<User>>(jsonString, options);
          if (users != null)
          {
            userList = users;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading users: {ex.Message}");
      }
    }
    
    public List<Teacher> GetAllTeachers()
    {
      return userList.OfType<Teacher>().ToList();
    }
    
    public List<Student> GetAllStudents()
    {
      return userList.OfType<Student>().ToList();
    }
    
    public Teacher? GetTeacherById(Guid id)
    {
      return userList.OfType<Teacher>().FirstOrDefault(t => t.Id == id);
    }
    
    public Student? GetStudentById(Guid id)
    {
      return userList.OfType<Student>().FirstOrDefault(s => s.Id == id);
    }
  }
}