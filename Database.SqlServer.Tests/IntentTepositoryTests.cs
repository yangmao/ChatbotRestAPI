using Database.SQLServer;
using Database.SQLServer.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Database.SqlServer.Tests
{
    public class IntentTepositoryTests
    {
        private readonly DapperContext _context;
        private readonly Mock<IConfiguration> _configurationMock;
        public IntentTepositoryTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            var configSectionMock = new Mock<IConfigurationSection>();
           
            _configurationMock.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(configSectionMock.Object);
            _context = new DapperContext(_configurationMock.Object);
        }
        [Fact]
        public void GetIntents()
        {
            string temp = "";
            var intentRepo = new IntentRepository(_context);
            var intents = intentRepo.GetIntents();

            Assert.NotNull(intents);
        }
    }
}