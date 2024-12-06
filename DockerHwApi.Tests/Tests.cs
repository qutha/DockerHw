using System.Globalization;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DockerHwApi.Tests;

public class Tests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetRoot_ReturnsHelloMessage()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello from Docker!", content);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(180, Math.PI)]
    [InlineData(360, 2 * Math.PI)]
    public async Task ConvertDegreesToRadians_ReturnsExpectedResult(double degrees, double expectedRadians)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/rad/{degrees}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(expectedRadians.ToString(CultureInfo.InvariantCulture), content);
    }
}
