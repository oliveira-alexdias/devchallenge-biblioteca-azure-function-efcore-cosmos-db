using System;
using System.Net;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace DevChallenge.Biblioteca.Functions
{
    public class RemoverObra
    {
        private readonly IObraRepository obraRepository;

        public RemoverObra(IObraRepository obraRepository)
        {
            this.obraRepository = obraRepository;
        }

        [Function(nameof(RemoverObra))]
        [OpenApiOperation(nameof(RemoverObra), Description = "")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(EmptyResult), Description = "Obra removida com sucesso")]
        [OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(EmptyResult), Description = "Obra não encontrada")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "obras/{id:guid}")] HttpRequestData req,
            Guid id,
            FunctionContext executionContext)
        {
            var obra = await obraRepository.ObterPorId(id);

            if (obra is null) return req.CreateResponse(HttpStatusCode.NotFound);

            await obraRepository.Remover(id);
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
