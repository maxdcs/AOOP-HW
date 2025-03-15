namespace UniversityManagement.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniversityManagement.Models;

public class SubjectManagerTests
{
    private readonly SubjectManager _subjectManager;
    private readonly Student _john;
    private readonly Student _jane;
    private readonly Teacher _dorito;
    private readonly Teacher _cheeto;

    public SubjectManagerTests()
    {
        this._john = new("John Smith", "john123", "123");
        this._jane = new("Jane Doe", "jane123", "123");

        this._dorito = new("Doro Rito", "doritos123", "123");
        this._cheeto = new("Cheet Oh", "cheetos123", "123");
        this._subjectManager = new();
    }

    [Theory]
    [InlineData("mathematics", "blalblalb")]
    [InlineData("mathematics", "213123123")]
    // [InlineData("history", "bleeh")]


    // -------------------Teacher Tests----------------------------
    public void Test_CreateNewSubject(string name, string desc)
    {

        // Act
        _subjectManager.CreateAndAddNewSubject(name, desc, _dorito.Id);

        // Assert
        Console.WriteLine(_subjectManager.subjectList[0].Name);
        Assert.Equal("mathematics", _subjectManager.subjectList[0].Name);

    }

    [Fact]
    public void Test_EditSubjectById()
    {
        //Arrange
        _subjectManager.CreateAndAddNewSubject("name", "desc", _dorito.Id);
        Guid id = _subjectManager.subjectList[0].Id;

        //Act
        _subjectManager.EditSubjectById(id, "newName", "newDesc");

        //Assert
        Assert.Equal("newName", _subjectManager.subjectList[0].Name);
        Assert.Equal("newDesc", _subjectManager.subjectList[0].Description);
    }

    [Fact]
    public void Test_DeleteSubjectById()
    {
        // Arrange
        _subjectManager.CreateAndAddNewSubject("name", "desc", _dorito.Id);
        Guid id = _subjectManager.subjectList[0].Id;


        // Act
        _subjectManager.DeleteSubjectById(id);

        // Assert
        Assert.Empty(_subjectManager.subjectList);
    }


    [Fact]
    public void Test_GetCreatedSubjectsByTeacherId()
    {
        // Arrange
        _subjectManager.CreateAndAddNewSubject("name1", "desc1", _dorito.Id);
        _subjectManager.CreateAndAddNewSubject("name2", "desc2", _dorito.Id);
        _subjectManager.CreateAndAddNewSubject("name3", "desc3", _dorito.Id);

        // Act
        int amount = _subjectManager.GetCreatedSubjectsByTeacherId(_dorito.Id).Count;

        // Assert
        Assert.Equal(3, amount);
    }



    // -------------------Student Tests----------------------------
    [Fact]
    public void Test_EnrollStudentInSubjectId()
    {
        // Arrange
        _subjectManager.CreateAndAddNewSubject("name1", "desc1", _dorito.Id);

        Subject subject = _subjectManager.subjectList[0];
        Guid subjectId = subject.Id;
        Guid studentId = _john.Id;

        // Act

        _subjectManager.EnrollStudentInSubjectId(studentId, subjectId);

        // Assert
        Assert.NotNull(subject.StudentsEnrolled);
        Assert.Equal(_john.Id, subject.StudentsEnrolled[0]);
    }

    [Fact]
    public void Test_RemoveSubjectFromStudent()
    {

        // Arrange
        _subjectManager.CreateAndAddNewSubject("name1", "desc1", _dorito.Id);
        Subject subject = _subjectManager.subjectList[0];
        Guid subjectId = subject.Id;
        Guid studentId = _john.Id;
        _subjectManager.EnrollStudentInSubjectId(studentId, subjectId);

        // Act
        _subjectManager.RemoveSubjectFromStudent(studentId, subjectId);

        // Assert
        Assert.NotNull(subject.StudentsEnrolled);
        Assert.Empty(subject.StudentsEnrolled);
    }

    [Fact]
    public void Test_GetEnrolledSubjectsByStudentId()
    {
        //Arrange
        _subjectManager.CreateAndAddNewSubject("name1", "desc1", _dorito.Id);
        _subjectManager.EnrollStudentInSubjectId(_john.Id, _subjectManager.subjectList[0].Id);
        // enroll student in that subject
        // call int result = getEnrolledSubjectsByStudentId(student.Id)
        // assert.equal(1, result)

        //Act
        int result = _subjectManager.GetEnrolledSubjectsByStudentId(_john.Id).Count;

        // Assert
        Assert.Equal(1, result);
    }
}
