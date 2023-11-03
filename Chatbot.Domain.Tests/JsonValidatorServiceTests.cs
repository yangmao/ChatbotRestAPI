using Chatbot.Domain.Concrete;

namespace Chatbot.Domain.Tests
{
    public class JsonValidatorServiceTests
    {
        [Fact]
        public void IsValidJson_ValidJson_ReturnsTrue()
        {
            // Arrange
            var jsonValidatorService = new JsonValidatorService();
            var validJson = "{\"key\": \"value\"}";

            // Act
            bool isValid = jsonValidatorService.IsValidJson(validJson);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void IsValidJson_InvalidJson_ReturnsFalse()
        {
            // Arrange
            var jsonValidatorService = new JsonValidatorService();
            var invalidJson = "Invalid JSON";

            // Act
            bool isValid = jsonValidatorService.IsValidJson(invalidJson);

            // Assert
            Assert.False(isValid);
        }
    }
}