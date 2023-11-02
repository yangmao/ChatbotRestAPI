using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using Chatbot.Domain.Services;
using ChatbotRestAPI.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChatbotRestAPI.Tests
{
    public class IntentControllerTests
    {
        private readonly Mock<ILogger<IntentController>> _loggerMock;
        private readonly Mock<IIntentRepository> _intentRepositoryMock;
        private readonly Mock<JsonValidatorService> _jsonValidatorServiceMock;

        public IntentControllerTests()
        {
            _loggerMock = new Mock<ILogger<IntentController>>();
            _intentRepositoryMock = new Mock<IIntentRepository>();
            _jsonValidatorServiceMock = new Mock<JsonValidatorService>();
        }

        [Fact]
        public async Task Create_ValidJson_ReturnsCreated()
        {
            // Arrange
            string userId = "testUserId";
            string validJson = "{\"key\": \"value\"}";
            var controller = new IntentController(_intentRepositoryMock.Object, _loggerMock.Object, _jsonValidatorServiceMock.Object);

            _jsonValidatorServiceMock.Setup(j => j.IsValidJson(validJson)).Returns(true);
            _intentRepositoryMock.Setup(r => r.AddIntents(userId, validJson)).Returns(Task.CompletedTask);
            _intentRepositoryMock.Setup(r => r.GetIntents(userId)).ReturnsAsync(new List<Intent>());

            // Act
            var result = await controller.Create(userId, validJson);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Create_InvalidJson_ReturnsBadRequest()
        {
            // Arrange
            string userId = "testUserId";
            string invalidJson = "invalid_json";
            var controller = new IntentController(_intentRepositoryMock.Object, _loggerMock.Object, _jsonValidatorServiceMock.Object);

            _jsonValidatorServiceMock.Setup(j => j.IsValidJson(invalidJson)).Returns(false);

            // Act
            var result = await controller.Create(userId, invalidJson);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}