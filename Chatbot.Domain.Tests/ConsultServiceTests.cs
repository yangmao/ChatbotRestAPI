using Chatbot.Domain.Concrete;
using Chatbot.Domain.Interface;
using Moq;

namespace Chatbot.Domain.Tests
{
    public class ConsultServiceTests
    {
        private readonly Mock<IWordEmbeddingService> _dataTransformerServiceMock;
        private readonly Mock<IHttpHandler> _HttpClientMock;

        public ConsultServiceTests()
        {
            _dataTransformerServiceMock = new Mock<IWordEmbeddingService>();
            _HttpClientMock = new Mock<IHttpHandler>();
        }
        
        //[Fact]
        //public void Given_InputMessage_Return_StringArray()
        //{
        //    _dataTransformerServiceMock.Setup(d => d.GetVacabulary()).ReturnsAsync(new string[] { "mock words"});
        //    _HttpClientMock.Setup(h => h.Client?.PostAsync).ReturnAsync(new HttpResponseMessage());
        //    var consultService = new ConsultService(_dataTransformerServiceMock.Object, _HttpClientMock.Object);
        //    var returns = consultService.Consult("Test");
        //    Assert.Equal("", returns.Result);
        //}
    }
}