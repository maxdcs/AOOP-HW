using System;
using System.Collections.ObjectModel;
using Avalonia.Headless.XUnit;
using Xunit;
using UniversityManagement.Models;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Tests
{
    public class StudentDroppingTests
    {
        [AvaloniaFact]
        public void Student_Can_Drop_A_Subject()
        {
            // Arrange
            var viewModel = new StudentWindowViewModel();
            var testSubject = new Subject("Test Subject", "Test Description", Guid.NewGuid());
            var testStudent = new Student("test", "testy", "123");
            viewModel.EnrolledSubjects = new ObservableCollection<Subject> { testSubject };
            viewModel.AvailableSubjects = new ObservableCollection<Subject>();
            viewModel.TemplateStudent = testStudent;
            
            // Act
            viewModel.EnrolledSubjects.Remove(testSubject);
            viewModel.AvailableSubjects.Add(testSubject);
            
            // Assert
            Assert.Contains(testSubject, viewModel.AvailableSubjects);
            Assert.DoesNotContain(testSubject, viewModel.EnrolledSubjects);
        }
    }
}