using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using FluentAssertions;
using Housemate.Application.Extensions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Housemate.Application.Tests.Extensions;

public sealed class AwsSecretsManagerExtensionsTests
{
    [Fact]
    public void AddAwsSecretsManager_WithValidCredentials_ShouldAddSecretsManagerToConfigurationBuilder()
    {
        // Arrange
        var configurationBuilder = new ConfigurationBuilder();
        var chainMock = new Mock<CredentialProfileStoreChain>();
        chainMock.Setup(c => c.TryGetAWSCredentials("Housemate", out It.Ref<AWSCredentials>.IsAny))
            .Returns(true);

        var credentials = new Mock<AWSCredentials>().Object;
        chainMock.Setup(c => c.TryGetAWSCredentials("Housemate", out credentials))
            .Returns(true);

        // Act
        var result = configurationBuilder.AddAwsSecretsManager();

        // Assert
        result.Should().BeSameAs(configurationBuilder);
    }

    [Fact]
    public void AddAwsSecretsManager_WithInvalidCredentials_ShouldReturnSameConfigurationBuilder()
    {
        // Arrange
        var configurationBuilder = new ConfigurationBuilder();
        var chainMock = new Mock<CredentialProfileStoreChain>();
        chainMock.Setup(c => c.TryGetAWSCredentials("Housemate", out It.Ref<AWSCredentials>.IsAny))
            .Returns(false);

        // Act
        var result = configurationBuilder.AddAwsSecretsManager();

        // Assert
        result.Should().BeSameAs(configurationBuilder);
    }
}