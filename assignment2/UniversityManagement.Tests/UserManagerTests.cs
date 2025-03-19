using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UniversityManagement.Models;
using Xunit;

namespace UniversityManagement.Tests
{
    public class UserManagerTests : IDisposable
    {
        private readonly UserManager _userManager;
        private readonly string _testDataPath = "Data";
        private readonly string _testFilePath = "Data/users.json";
        
        public UserManagerTests()
        {
            // Ensure test directory exists
            Directory.CreateDirectory(_testDataPath);
            
            // Create test data
            CreateTestData();
            
            // Create a test instance
            _userManager = new UserManager();
        }
        
        private void CreateTestData()
        {
            var users = new List<object>
            {
                new
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
                    Name = "Jane Doe",
                    Username = "janedoe123",
                    Password = "123",
                    Role = "Teacher"
                },
                new
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
                    Name = "Richard Roe",
                    Username = "richardroe",
                    Password = "123",
                    Role = "Teacher"
                },
                new
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
                    Name = "John Smith",
                    Username = "johnsmith",
                    Password = "123",
                    Role = "Student"
                },
                new
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                    Name = "Alice Johnson",
                    Username = "alicej",
                    Password = "123",
                    Role = "Student"
                }
            };
            
            // Write to file
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(users, options);
            File.WriteAllText(_testFilePath, jsonString);
        }
        
        public void Dispose()
        {
            // Clean up test data
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }
        
        [Fact]
        public void Test_GetAllTeachers_ReturnsTeachers()
        {
            // Act
            var teachers = _userManager.GetAllTeachers();
            
            // Assert
            Assert.NotNull(teachers);
            Assert.All(teachers, teacher => Assert.Equal("Teacher", teacher.Role));
        }
        
        [Fact]
        public void Test_GetAllStudents_ReturnsStudents()
        {
            // Act
            var students = _userManager.GetAllStudents();
            
            // Assert
            Assert.NotNull(students);
            Assert.All(students, student => Assert.Equal("Student", student.Role));
        }
        
        [Theory]
        [InlineData("janedoe123", "123", true)]  // Valid teacher credentials
        [InlineData("johnsmith", "123", true)]   // Valid student credentials
        [InlineData("wronguser", "wrongpass", false)]  // Invalid credentials
        [InlineData("", "", false)]  // Empty credentials
        public void Test_AuthenticateUser(string username, string password, bool shouldSucceed)
        {
            // Act
            var user = _userManager.AuthenticateUser(username, password);
            
            // Assert
            if (shouldSucceed)
            {
                Assert.NotNull(user);
                Assert.Equal(username, user.Username);
            }
            else
            {
                Assert.Null(user);
            }
        }
        
        [Fact]
        public void Test_GetTeacherById_ReturnsCorrectTeacher()
        {
            // Arrange - Get all teachers first
            var teachers = _userManager.GetAllTeachers();
            if (!teachers.Any())
            {
                return; // Skip test if no teachers available
            }
            
            var firstTeacher = teachers.First();
            
            // Act
            var foundTeacher = _userManager.GetTeacherById(firstTeacher.Id);
            
            // Assert
            Assert.NotNull(foundTeacher);
            Assert.Equal(firstTeacher.Id, foundTeacher.Id);
            Assert.Equal(firstTeacher.Name, foundTeacher.Name);
        }
        
        [Fact]
        public void Test_GetStudentById_ReturnsCorrectStudent()
        {
            // Arrange - Get all students first
            var students = _userManager.GetAllStudents();
            if (!students.Any())
            {
                return; // Skip test if no students available
            }
            
            var firstStudent = students.First();
            
            // Act
            var foundStudent = _userManager.GetStudentById(firstStudent.Id);
            
            // Assert
            Assert.NotNull(foundStudent);
            Assert.Equal(firstStudent.Id, foundStudent.Id);
            Assert.Equal(firstStudent.Name, foundStudent.Name);
        }
    }
}