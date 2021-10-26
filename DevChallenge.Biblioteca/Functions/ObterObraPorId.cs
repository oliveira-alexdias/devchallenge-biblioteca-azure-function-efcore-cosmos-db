using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Data.Repository;
using DevChallenge.Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace DevChallenge.Biblioteca.Functions
{
    public class ObterObraPorId
    {
        private readonly IObraRepository obraRepository;

        public ObterObraPorId(IObraRepository obraRepository)
        {
            this.obraRepository = obraRepository;
        }

        [Function(nameof(ObterObraPorId))]
        [OpenApiOperation("ObterObraPorId", Description = "")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ObterObraModel), Description = "Dados da obra solicitada")]
        [OpenApiResponseWithBody(HttpStatusCode.NoContent, "application/json", typeof(EmptyResult), Description = "Obra não encontrada")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "obras/{id:guid}")] HttpRequestData req,
            Guid id,
            FunctionContext executionContext)
        {
            var obra = await obraRepository.ObterPorId(id);

            if (obra is null)
            {
                return req.CreateResponse(HttpStatusCode.NoContent);
            }

            var model = new ObterObraModel(obra);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(model);

            return response;
        }
    }
}
