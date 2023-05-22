using FluentAssertions;
using Housemate.Application.Helpers;
using Xunit;

namespace Housemate.Application.Tests.Helpers;

public sealed class CacheKeysTests
{
    [Fact]
    public void Student_GetAll_ShouldReturnCorrectValue()
    {
        // Arrange
        var expectedKey = "students-all";

        // Act
        var cacheKey = CacheKeys.Student.GetAll;

        // Assert
        cacheKey.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("1", "students-1")]
    [InlineData("abc", "students-abc")]
    public void Student_Get_ShouldReturnCorrectValue(string id, string expectedKey)
    {
        // Act
        var cacheKey = CacheKeys.Student.Get(id);

        // Assert
        cacheKey.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("test@example.com", "students-email-test@example.com")]
    [InlineData("user@example.com", "students-email-user@example.com")]
    public void Student_GetByEmail_ShouldReturnCorrectValue(string email, string expectedKey)
    {
        // Act
        var cacheKey = CacheKeys.Student.GetByEmail(email);

        // Assert
        cacheKey.Should().Be(expectedKey);
    }

    [Fact]
    public void HousingTask_GetAll_ShouldReturnCorrectValue()
    {
        // Arrange
        var expectedKey = "housing-tasks-all";

        // Act
        var cacheKey = CacheKeys.HousingTask.GetAll;

        // Assert
        cacheKey.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("1", "housing-tasks-1")]
    [InlineData("abc", "housing-tasks-abc")]
    public void HousingTask_Get_ShouldReturnCorrectValue(string id, string expectedKey)
    {
        // Act
        var cacheKey = CacheKeys.HousingTask.Get(id);

        // Assert
        cacheKey.Should().Be(expectedKey);
    }

    [Fact]
    public void Waste_GetAll_ShouldReturnCorrectValue()
    {
        // Arrange
        var expectedKey = "wastes-all";

        // Act
        var cacheKey = CacheKeys.Waste.GetAll;

        // Assert
        cacheKey.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("1", "wastes-1")]
    [InlineData("abc", "wastes-abc")]
    public void Waste_Get_ShouldReturnCorrectValue(string id, string expectedKey)
    {
        // Act
        var cacheKey = CacheKeys.Waste.Get(id);

        // Assert
        cacheKey.Should().Be(expectedKey);
    }
}