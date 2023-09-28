using Chatbot.Domain.Interface;
using ChatbotAPI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChatbotRestAPI.Tests
{
    public class ChatControllerTests
    {
        private readonly Mock<ILogger<ChatController>> _loggerMock;
        private readonly Mock<IConsultService> _consultServiceMock;

        public ChatControllerTests()
        {
            _loggerMock = new Mock<ILogger<ChatController>>();
            _consultServiceMock = new Mock<IConsultService>();
        }
        [Fact]
        public void Given_NullMsg_Return_ErrorMessage()
        {
            _consultServiceMock.Setup(c => c.Consult(It.IsAny<string>(),It.IsAny<string>())).Throws<ArgumentNullException>();
            var controller = new ChatController(_consultServiceMock.Object, _loggerMock.Object);
            Assert.ThrowsAsync<Exception>(()=> controller.Chat("userid",null));
        }
    }
}