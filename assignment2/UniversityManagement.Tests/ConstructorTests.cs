namespace UniversityManagement.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniversityManagement.Models;

public class ConstructorTests
{
    [Fact]
    public void Test_StudentConstructor()
    {
        // Arrange
        string studentName = "Max";
        string username = "max";
        string password = "123";


        // Act
        Student studentMax = new(studentName, username, password);


        // Assert
        Assert.Equal("max", studentMax.Username);
    }

    [Fact]
    public void Test_TeacherConstructor()
    {
        // Arrange
        string teacherName = "Dori";
        string username = "fish";
        string password = "123";


        // Act
        Teacher teacherDorthe = new(teacherName, username, password);


        // Assert
        Assert.Equal("fish", teacherDorthe.Username);
    }

    [Fact]
    public void Test_SubjectConstructor()
    {
        // Arrange
        string name = "Advanced Oriental Programming";
        string description = "Advanced course in chinese programming";
        Guid teacherId = new();

        // Act
        Subject advOrientalProgramming = new(name, description, teacherId);

        // Assert
        Assert.Equal("Advanced Oriental Programming", advOrientalProgramming.Name);
    }

    [Fact]
    public void Test_SubjectManagerConstructor()
    {
        // Arrange
        SubjectManager subjectManager = new();

        // Assert
        Assert.Empty(subjectManager.subjectList);
    }


}
