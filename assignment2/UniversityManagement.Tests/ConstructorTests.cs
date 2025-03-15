namespace UniversityManagement.Tests;

using UniversityManagement.Models;

public class UnitTest1
{
    [Fact]
    public void Test_StudentConstructor()
    {
        // Arrange
        int a = 3;
        int b = 5;

        // Act
        int result = a + b;

        // Assert
        Assert.Equal(8, result);
    }
}
