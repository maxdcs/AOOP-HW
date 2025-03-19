using System;
using System.Collections.ObjectModel;
using Avalonia.Headless.XUnit;
using Xunit;
using UniversityManagement.Models;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Tests
{
    public class TeacherCreationTests
    {
        [AvaloniaFact]
        public void Teacher_Can_Create_New_Subject()
        {
            // Arrange
            var viewModel = new TeacherWindowViewModel();
            var testSubject = new Subject("Test Subject", "Test Description", Guid.NewGuid());
            viewModel.SubjectList = new ObservableCollection<Subject>();
            
            // Act
            viewModel.SubjectList.Add(testSubject);
            
            // Assert
            Assert.Contains(viewModel.SubjectList, s => s.Name == "Test Subject");
            Assert.Single(viewModel.SubjectList);
        }
    }
}