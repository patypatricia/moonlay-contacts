using Azure.WebJobs.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Moonlay.Contacts.Application;
using Moonlay.Contacts.Models;
using Moonlay.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Moonlay.Faas.Contacts
{
    public static class ContactFunction
    {
        [FunctionName("Contacts")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Inject]IContactService service,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestId = req.Query["request_id"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            IActionResult response;

            if(req.Method.ToLower() == "get")
            {
                var listOfContacts = await service.FindAllAsync(0, 25);

                var result = new GenericResponse<IEnumerable<ContactDto>>(true, listOfContacts.Select(o => new ContactDto { Id = o.Id, Names = o.Names }))
                {
                    RequestId = requestId ?? data?.request_id
                };

                response = new OkObjectResult(result);
            }
            else
            {
                response = new OkObjectResult("");
            }

            return response;
        }
    }
}
