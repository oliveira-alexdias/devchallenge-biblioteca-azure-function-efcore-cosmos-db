using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Data.Repository;
using DevChallenge.Biblioteca.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace DevChallenge.Biblioteca.Functions
{
    public class ObterTodasAsObras
    {
        private readonly IObraRepository obraRepository;

        public ObterTodasAsObras(IObraRepository obraRepository)
        {
            this.obraRepository = obraRepository;
        }

        [Function(nameof(ObterTodasAsObras))]
        [OpenApiOperation(nameof(ObterTodasAsObras), Description = "")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IList<ObterObraModel>), Description = "Todas as obras registradas no sistema")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "obras")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var obras = await obraRepository.ObterTodos();
            var model = obras.Select(o => new ObterObraModel(o)).ToList();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(model);

            return response;
        }
    }
}
