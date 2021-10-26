using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Data.Repository;
using DevChallenge.Biblioteca.Entities;
using DevChallenge.Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace DevChallenge.Biblioteca.Functions
{
    public class AtualizarObra
    {
        private readonly IObraRepository obraRepository;

        public AtualizarObra(IObraRepository obraRepository)
        {
            this.obraRepository = obraRepository;
        }

        [Function(nameof(AtualizarObra))]
        [OpenApiOperation(nameof(AtualizarObra), Description = "")]
        [OpenApiRequestBody("application/json", typeof(AtualizarObraModel), Description = "")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "")]
        [OpenApiResponseWithBody(HttpStatusCode.NoContent, "application/json", typeof(EmptyResult), Description = "Obra atualizada com sucesso")]
        [OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(EmptyResult), Description = "Obra não encontrada")]
        [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(List<string>), Description = "Requisição inválida")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PUT", Route = "obras/{id:guid}")] HttpRequestData req,
            Guid id,
            FunctionContext executionContext)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var model = new AtualizarObraModel(body);

            if (!model.EstaValida())
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteAsJsonAsync(model.ObterErros());
                return badRequest;
            }

            var obra = await obraRepository.ObterPorId(id);

            if (obra is null) return req.CreateResponse(HttpStatusCode.NotFound);

            obra.Titulo = model.Titulo;
            obra.Editora = model.Editora;
            obra.Foto = model.Foto;
            obra.Autores = model.Autores.Select(a => new Autor(a)).ToList();
            await obraRepository.Atualizar(obra);

            return req.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
