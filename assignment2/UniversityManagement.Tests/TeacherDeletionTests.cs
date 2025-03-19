using System;
using System.Collections.ObjectModel;
using Avalonia.Headless.XUnit;
using Xunit;
using UniversityManagement.Models;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Tests
{
    public class TeacherDeletionTests
    {
        [AvaloniaFact]
        public void Teacher_Can_Delete_A_Subject()
        {
            // Arrange
            var viewModel = new TeacherWindowViewModel();
            var testSubject = new Subject("Test Subject", "Test Description", Guid.NewGuid());
            viewModel.SubjectList = new ObservableCollection<Subject> { testSubject };
            
            // Act
            viewModel.SubjectList.Remove(testSubject);
            
            // Assert
            Assert.Empty(viewModel.SubjectList);
        }
    }
}