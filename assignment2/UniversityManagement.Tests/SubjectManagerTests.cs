using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UniversityManagement.Models;
using Xunit;

namespace UniversityManagement.Tests
{
    public class SubjectManagerTests : IDisposable
    {
        private readonly SubjectManager _subjectManager;
        private readonly string _testDataPath = "Data";
        private readonly string _testFilePath = "Data/subjects.json";
        private readonly Guid _teacherId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
        private readonly Guid _studentId = Guid.Parse("550e8400-e29b-41d4-a716-446655440002");
        
        public SubjectManagerTests()
        {
            // Ensure test directory exists
            Directory.CreateDirectory(_testDataPath);
            
            // Create test data
            CreateTestData();
            
            // Create a test instance
            _subjectManager = new SubjectManager();
        }
        
        private void CreateTestData()
        {
            var subjects = new List<object>
            {
                new
                {
                    Id = Guid.Parse("650e8400-e29b-41d4-a716-446655440001"),
                    Name = "Test Subject 1",
                    Description = "Test Description 1",
                    TeacherId = _teacherId,
                    StudentsEnrolled = new List<Guid>()
                },
                new
                {
                    Id = Guid.Parse("650e8400-e29b-41d4-a716-446655440002"),
                    Name = "Test Subject 2",
                    Description = "Test Description 2",
                    TeacherId = _teacherId,
                    StudentsEnrolled = new List<Guid> { _studentId }
                },
                new
                {
                    Id = Guid.Parse("650e8400-e29b-41d4-a716-446655440003"),
                    Name = "Test Subject 3",
                    Description = "Test Description 3",
                    TeacherId = Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
                    StudentsEnrolled = new List<Guid>()
                }
            };
            
            // Write to file
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(subjects, options);
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
        public void Test_CreateAndAddNewSubject_AddsSubject()
        {
            // Arrange
            int initialCount = _subjectManager.subjectList.Count;
            string name = "New Test Subject";
            string description = "New Test Description";
            
            // Act
            _subjectManager.CreateAndAddNewSubject(name, description, _teacherId);
            
            // Assert
            Assert.Equal(initialCount + 1, _subjectManager.subjectList.Count);
            
            var addedSubject = _subjectManager.subjectList.LastOrDefault();
            Assert.NotNull(addedSubject);
            Assert.Equal(name, addedSubject.Name);
            Assert.Equal(description, addedSubject.Description);
            Assert.Equal(_teacherId, addedSubject.TeacherId);
        }
        
        [Fact]
        public void Test_EditSubjectById_UpdatesSubject()
        {
            // Arrange
            var subject = _subjectManager.subjectList.First();
            string newName = "Updated Name";
            string newDescription = "Updated Description";
            
            // Act
            _subjectManager.EditSubjectById(subject.Id, newName, newDescription);
            
            // Assert
            var updatedSubject = _subjectManager.subjectList.First(s => s.Id == subject.Id);
            Assert.Equal(newName, updatedSubject.Name);
            Assert.Equal(newDescription, updatedSubject.Description);
        }
        
        [Fact]
        public void Test_DeleteSubjectById_RemovesSubject()
        {
            // Arrange
            var subject = _subjectManager.subjectList.First();
            int initialCount = _subjectManager.subjectList.Count;
            
            // Act
            _subjectManager.DeleteSubjectById(subject.Id);
            
            // Assert
            Assert.Equal(initialCount - 1, _subjectManager.subjectList.Count);
            Assert.DoesNotContain(_subjectManager.subjectList, s => s.Id == subject.Id);
        }
        
        [Fact]
        public void Test_EnrollStudentInSubjectId_AddsStudentToSubject()
        {
            // Arrange
            var subject = _subjectManager.subjectList.First();
            
            // Act
            _subjectManager.EnrollStudentInSubjectId(_studentId, subject.Id);
            
            // Assert
            var updatedSubject = _subjectManager.subjectList.First(s => s.Id == subject.Id);
            Assert.Contains(_studentId, updatedSubject.StudentsEnrolled);
        }
        
        [Fact]
        public void Test_RemoveSubjectFromStudent_RemovesStudentFromSubject()
        {
            // Arrange - Find a subject with our student
            var subject = _subjectManager.subjectList.FirstOrDefault(s => s.StudentsEnrolled.Contains(_studentId));
            if (subject == null)
            {
                // Create a subject with our student if none exists
                subject = _subjectManager.subjectList.First();
                _subjectManager.EnrollStudentInSubjectId(_studentId, subject.Id);
            }
            
            // Act
            _subjectManager.RemoveSubjectFromStudent(_studentId, subject.Id);
            
            // Assert
            var updatedSubject = _subjectManager.subjectList.First(s => s.Id == subject.Id);
            Assert.DoesNotContain(_studentId, updatedSubject.StudentsEnrolled);
        }
        
        [Fact]
        public void Test_GetCreatedSubjectsByTeacherId_ReturnsTeacherSubjects()
        {
            // Act
            var teacherSubjects = _subjectManager.GetCreatedSubjectsByTeacherId(_teacherId);
            
            // Assert
            Assert.NotEmpty(teacherSubjects);
            Assert.All(teacherSubjects, subject => Assert.Equal(_teacherId, subject.TeacherId));
        }
        
        [Fact]
        public void Test_GetEnrolledSubjectsByStudentId_ReturnsEnrolledSubjects()
        {
            // Arrange - Make sure student is enrolled in at least one subject
            var subjectToEnroll = _subjectManager.subjectList.First();
            _subjectManager.EnrollStudentInSubjectId(_studentId, subjectToEnroll.Id);
            
            // Act
            var enrolledSubjects = _subjectManager.GetEnrolledSubjectsByStudentId(_studentId);
            
            // Assert
            Assert.NotEmpty(enrolledSubjects);
            Assert.All(enrolledSubjects, subject => Assert.Contains(_studentId, subject.StudentsEnrolled));
        }
    }
}