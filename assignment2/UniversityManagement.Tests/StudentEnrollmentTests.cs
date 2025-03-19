using System;
using System.Collections.ObjectModel;
using Avalonia.Headless.XUnit;
using Xunit;
using UniversityManagement.Models;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Tests
{
    public class StudentEnrollmentTests
    {
        [AvaloniaFact]
        public void Student_Can_Enroll_In_Subject()
        {
            // Arrange
            var viewModel = new StudentWindowViewModel();
            var testSubject = new Subject("Test Subject", "Test Description", Guid.NewGuid());
            var testStudent = new Student("test", "testy", "123");
            viewModel.AvailableSubjects = new ObservableCollection<Subject> { testSubject };
            viewModel.EnrolledSubjects = new ObservableCollection<Subject>();
            viewModel.TemplateStudent = testStudent;
            
            // Act
            viewModel.AvailableSubjects.Remove(testSubject);
            viewModel.EnrolledSubjects.Add(testSubject);
            
            // Assert
            Assert.Contains(testSubject, viewModel.EnrolledSubjects);
            Assert.DoesNotContain(testSubject, viewModel.AvailableSubjects);
        }
    }
}