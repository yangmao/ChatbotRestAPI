using Moq; 
using Chatbot.Domain.Interface;
using ChatbotRestAPI.Controller;
using Microsoft.AspNetCore.Mvc;
using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;

public class IntentControllerTests
{
    [Fact]
    public async Task Create_ValidJson_Success()
    {
        // Arrange
        var userId = "testUser";
        var validJson = "{\"intent1\":[{\"tag\":\"tag1\"}]}";
        var intentRepositoryMock = new Mock<IIntentRepository>();
        var jsonValidatorServiceMock = new Mock<IJsonValidatorService>();
        intentRepositoryMock.Setup(repo => repo.AddIntents(userId, validJson)).Returns(Task.CompletedTask);
        jsonValidatorServiceMock.Setup(service => service.IsValidJson(validJson)).Returns(true);

        var controller = new IntentController(intentRepositoryMock.Object, null, jsonValidatorServiceMock.Object);

        // Act
        var result = await controller.Create(userId, validJson) as CreatedResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("/Create", result.Location);
    }

    [Fact]
    public async Task Create_InvalidJson_BadRequest()
    {
        // Arrange
        var userId = "testUser";
        var invalidJson = "invalid-json";
        var jsonValidatorServiceMock = new Mock<IJsonValidatorService>();
        jsonValidatorServiceMock.Setup(service => service.IsValidJson(invalidJson)).Returns(false);

        var controller = new IntentController(null, null, jsonValidatorServiceMock.Object);

        // Act
        var result = await controller.Create(userId, invalidJson) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Invalid JSON format", result.Value);
    }

    [Fact]
    public async Task Get_ExistingIntents_Success()
    {
        // Arrange
        var userId = "testUser";
        var intents = new List<Intent> { new Intent { Tag = "tag1" } };
        var intentRepositoryMock = new Mock<IIntentRepository>();
        intentRepositoryMock.Setup(repo => repo.GetIntents(userId)).ReturnsAsync(intents);

        var controller = new IntentController(intentRepositoryMock.Object, null, null);

        // Act
        var result = await controller.Get(userId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var resultValue = result.Value as List<Intent>;
        Assert.NotNull(resultValue);
        Assert.Single(resultValue);
    }

    [Fact]
    public async Task Get_NonExistingIntents_NotFound()
    {
        // Arrange
        var userId = "testUser";
        List<Intent> intents = null;
        var intentRepositoryMock = new Mock<IIntentRepository>();
        intentRepositoryMock.Setup(repo => repo.GetIntents(userId)).ReturnsAsync(intents);

        var controller = new IntentController(intentRepositoryMock.Object, null, null);

        // Act
        var result = await controller.Get(userId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
    }

    // Add similar tests for Update and Delete actions
}

