using Database.MongoDb.Adapter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Database.MongoDb.Adapter
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IIntentContext _intentContext;
        public TestController(IIntentContext context) 
        {
            _intentContext = context;
        }

        public IntentsDto Get(int id)
        {
            return _intentContext.Get(id);
        }
    }
}
