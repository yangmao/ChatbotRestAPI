using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public Task AddIntent(Intent intent)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Intent>> GetIntents()
        {
            var query = "SELECT * FROM Intent";
            using (var connection = _context.CreateConnection())
            {
                return  await connection.QueryAsync<Intent>(query);
            }
        }

        public Task RemoveIntent(string tag)
        {
            throw new NotImplementedException();
        }

        public Task UpdateIntent(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}
