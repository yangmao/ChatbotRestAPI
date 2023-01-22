﻿using Chatbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interface
{
    public interface IDataTransformerService
    {
        Task<List<Intent>> GetIntents();
        Task<string[]> GetWords();

        Task<string[]> GetLables();

        Task<string> GetTraining();
    }
}