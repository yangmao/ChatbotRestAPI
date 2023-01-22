using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Database.SQLServer.Repositories
{
    public class IntentRepository : IIntentRepository
    {
        private readonly IDapperContext _context ;
        public IntentRepository(IDapperContext context)
        { 
            _context= context;
        }
        public async Task<List<Intent>> GetIntents()
        {
            var query = "SELECT * FROM Intent";
            using (var connection = _context.CreateConnection())
            {
                var intents = await connection.QueryAsync<Intent>(query);
                return intents.ToList();
            }
        }
    }
}
