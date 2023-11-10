using Chatbot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chatbot.Domain.Ports;
using ChatbotRestAPI.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Chatbot.Domain.Interface;

public class IntentControllerTests
{
    [Fact]
    public async Task Create_ValidJson_ReturnsCreated()
    {
        // Arrange
        var userId = "testUser";
        var json = JsonConvert.SerializeObject(new Intent { Tag = "TestTag", Pattern = new List<string>(), Response = new List<string>() });

        var mockIntentRepository = new Mock<IIntentRepository>();
        mockIntentRepository.Setup(repo => repo.AddIntents(userId, json))
                           .Returns(Task.CompletedTask);
        mockIntentRepository.Setup(repo => repo.GetIntents(userId))
                           .ReturnsAsync(new List<Intent> { new Intent { Tag = "TestTag", Pattern = new List<string>(), Response = new List<string>() } });

        var mockJsonValidatorService = new Mock<IJsonValidatorService>();
        mockJsonValidatorService.Setup(validator => validator.IsValidJson(It.IsAny<string>()))
                                .Returns(true);

        var controller = new IntentController(mockIntentRepository.Object, Mock.Of<ILogger<IntentController>>(), mockJsonValidatorService.Object);

        // Act
        var result = await controller.Create(userId, json);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("/Create", createdResult.Location);
        var intentsObject = Assert.IsType<List<Intent>>(createdResult.Value);
        Assert.Single(intentsObject);
    }

    [Fact]
    public async Task AddIntent_UniqueTag_ReturnsCreated()
    {
        // Arrange
        var userId = "testUser";
        var json = JsonConvert.SerializeObject(new Intent { Tag = "NewTag", Pattern = new List<string>(), Response = new List<string>() });

        var mockIntentRepository = new Mock<IIntentRepository>();
        mockIntentRepository.Setup(repo => repo.GetIntents(userId))
                           .ReturnsAsync(new List<Intent> { new Intent { Tag = "ExistingTag", Pattern = new List<string>(), Response = new List<string>() } });

        var mockJsonValidatorService = new Mock<IJsonValidatorService>();
        mockJsonValidatorService.Setup(validator => validator.IsValidJson(It.IsAny<string>()))
                                .Returns(true);

        var controller = new IntentController(mockIntentRepository.Object, Mock.Of<ILogger<IntentController>>(), mockJsonValidatorService.Object);

        // Act
        var result = await controller.AddIntent(userId, json);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("/AddIntent", createdResult.Location);
        var intentsObject = Assert.IsType<List<Intent>>(createdResult.Value);
        Assert.Single(intentsObject);
    }

    [Fact]
    public async Task AddIntentFlow_UniqueTag_ReturnsCreated()
    {
        // Arrange
        var userId = "testUser";
        var json = JsonConvert.SerializeObject(new Intent { Tag = "NewTag", Pattern = new List<string>(), Response = new List<string>() });

        var mockIntentRepository = new Mock<IIntentRepository>();
        mockIntentRepository.Setup(repo => repo.GetIntents(userId))
                           .ReturnsAsync(new List<Intent> { new Intent { Tag = "ExistingTag", Pattern = new List<string>(), Response = new List<string>() } });
        mockIntentRepository.Setup(repo => repo.AddIntents(userId, json))
                           .Returns(Task.CompletedTask);

        var mockJsonValidatorService = new Mock<IJsonValidatorService>();
        mockJsonValidatorService.Setup(validator => validator.IsValidJson(It.IsAny<string>()))
                                .Returns(true);

        var controller = new IntentController(mockIntentRepository.Object, Mock.Of<ILogger<IntentController>>(), mockJsonValidatorService.Object);

        // Act
        var result = await controller.AddIntent(userId, json);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal("/AddIntent", createdResult.Location);
        var intentsObject = Assert.IsType<List<Intent>>(createdResult.Value);
        Assert.Single(intentsObject);
    }

    [Fact]
    public async Task UpdateFlow_ReturnsOk()
    {
        // Arrange
        var userId = "testUser";
        var json = JsonConvert.SerializeObject(new Intent { Tag = "ExistingTag", Pattern = new List<string>(), Response = new List<string>() });

        var mockIntentRepository = new Mock<IIntentRepository>();
        mockIntentRepository.Setup(repo => repo.UpsertIntent(userId, json))
                           .Returns(Task.CompletedTask);

        var controller = new IntentController(mockIntentRepository.Object, Mock.Of<ILogger<IntentController>>(), Mock.Of<IJsonValidatorService>());

        // Act
        var result = await controller.Update(userId, json);

        // Assert
        Assert.IsType<OkResult>(result);
    }

}