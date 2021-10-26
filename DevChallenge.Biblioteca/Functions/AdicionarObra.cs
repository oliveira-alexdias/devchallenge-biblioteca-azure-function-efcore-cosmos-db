using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Data.Repository;
using DevChallenge.Biblioteca.Entities;
using DevChallenge.Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace DevChallenge.Biblioteca.Functions
{
    public class AdicionarObra
    {
        private readonly IObraRepository obraRepository;

        public AdicionarObra(IObraRepository obraRepository)
        {
            this.obraRepository = obraRepository;
        }

        [Function(nameof(AdicionarObra))]
        [OpenApiOperation(nameof(AdicionarObra), Description = "")]
        [OpenApiRequestBody("application/json", typeof(AdicionarObraBaseModel), Description = "")]
        [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(Obra), Description = "Obra adicionada com sucesso")]
        [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(List<string>), Description = "Requisição inválida")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "obras")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var model = new AdicionarObraBaseModel(body);

            if (!model.EstaValida())
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteAsJsonAsync(model.ObterErros());
                return badRequest;
            }

            var obra = new Obra(model.Titulo, model.Editora, model.Foto, model.Autores);
            await obraRepository.Adicionar(obra);
            
            var response = req.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("location", req.Url.AbsolutePath);
            await response.WriteAsJsonAsync(obra);

            return response;
        }
    }
}
