using Chatbot.Domain.Interface;
using System;
using System.Net.Http;

namespace Chatbot.Domain.concrete
{
    public class ConsultService : IConsultService
    {
        public string Consult(string query)
        {
            var httpClient = new HttpClient();
            var modelUrl = "https://chatbot-model.herokuapp.com/v1/models/chatbot_model:predict";

            var vector = NLPHelper.BagOfWords("", null);
            return "";
            //var content = new FormUrlEncodedContent(vector);
            //var results = httpClient.PostAsync(modelUrl,);

            //    results.Result.

            //results_index = numpy.argmax(results)
            //if (numpy.max(results) < 0.70):
            //    tag = "unknown"
            //else:
            //    tag = labels[results_index]
            //print("The tag is:", tag)
            //#response
            //response = []
            //for itt in data["intents"]:
            //    if itt['tag'] == tag:
            //        response = itt['responses']
            //print("The final response is: ", response)

            //return random.choice(response)
            //    return "";
        }
    }
}
