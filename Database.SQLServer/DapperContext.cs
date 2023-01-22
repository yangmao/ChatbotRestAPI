using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Chatbot.Domain.Ports;

namespace Database.SQLServer
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AppConnectionString");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
