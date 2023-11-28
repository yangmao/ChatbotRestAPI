using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using ChatbotRestAPI.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Chatbot.Domain.Interface;

namespace ChatbotRestAPI.Tests
{
    public class IntentControllerTests
    {
        private readonly Mock<ILogger<IntentController>> _loggerMock;
        private readonly Mock<IIntentRepository> _intentRepositoryMock;
        private readonly Mock<IJsonValidatorService> _jsonValidatorServiceMock;

        public IntentControllerTests()
        {
            _loggerMock = new Mock<ILogger<IntentController>>();
            _intentRepositoryMock = new Mock<IIntentRepository>();
            _jsonValidatorServiceMock = new Mock<IJsonValidatorService>();
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
            Assert.IsType<CreatedResult>(result);
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

        [Fact]
        public async Task DeleteAll_ReturnsOkResult()
        {
            // Arrange
            var userId = "testUserId";
            var mockLogger = new Mock<ILogger<IntentController>>();
            var mockIntentRepository = new Mock<IIntentRepository>();

            var intentController = new IntentController(mockIntentRepository.Object, mockLogger.Object, null);

            // Act
            var result = await intentController.DeleteAll(userId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteAll_CallsRemoveAllIntents()
        {
            // Arrange
            var userId = "testUserId";
            var mockLogger = new Mock<ILogger<IntentController>>();
            var mockIntentRepository = new Mock<IIntentRepository>();

            var intentController = new IntentController(mockIntentRepository.Object, mockLogger.Object, null);

            // Act
            await intentController.DeleteAll(userId);

            // Assert
            mockIntentRepository.Verify(x => x.RemoveAllIntents(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteAll_ReturnsBadRequestOnException()
        {
            // Arrange
            var userId = "testUserId";
            var mockLogger = new Mock<ILogger<IntentController>>();
            var mockIntentRepository = new Mock<IIntentRepository>();
            mockIntentRepository.Setup(x => x.RemoveAllIntents(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));

            var intentController = new IntentController(mockIntentRepository.Object, mockLogger.Object, null);

            // Act
            var result = await intentController.DeleteAll(userId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}