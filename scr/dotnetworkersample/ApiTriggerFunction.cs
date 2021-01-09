using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using System.Net;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker.Pipeline;

namespace FunctionAppSample
{
    public class ApiTriggerFunction
    {
        private readonly IHttpResponderService _responderService;

        public ApiTriggerFunction(IHttpResponderService responderService)
        {
            _responderService = responderService;
        }

        [FunctionName("ApiTriggerFunction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
            FunctionExecutionContext executionContext)
        {
            var log = executionContext.Logger;
            log.LogInformation("message logged");

            
            return _responderService.ProcessRequest(req);
        }

        public interface IHttpResponderService
        {
            HttpResponseData ProcessRequest(HttpRequestData httpRequest);
        }

        public class DefaultHttpResponderService : IHttpResponderService
        {
            public DefaultHttpResponderService()
            {

            }

            public HttpResponseData ProcessRequest(HttpRequestData httpRequest)
            {

                string name = httpRequest.Query["name"];
                var response = new HttpResponseData(HttpStatusCode.OK);
                var headers = new Dictionary<string, string>();
                headers.Add("Date", "Mon, 18 Jul 2016 16:06:00 GMT");
                headers.Add("Content", "Content - Type: text / html; charset = utf - 8");

                response.Headers = headers;
                response.Body = $"Welcome to .NET 5 Functions Worker, {name}!!";

                return response;
            }
        }
    }
}
