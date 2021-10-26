using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using FluentValidation;
using Newtonsoft.Json;

namespace DevChallenge.Biblioteca.Models
{
    public class AdicionarObraBaseModel : BaseModel
    {
        public AdicionarObraBaseModel()
        {
        }

        public AdicionarObraBaseModel(string body)
        {
            var model = JsonConvert.DeserializeObject<AdicionarObraBaseModel>(body) ?? new AdicionarObraBaseModel();

            Titulo = model.Titulo;
            Editora = model.Editora;
            Foto = model.Foto;
            Autores = model.Autores.Select(a => a).ToList();

            Validar();
        }

        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string Foto { get; set; }
        public IList<string> Autores { get; set; } = new List<string>();

        private void Validar()
        {
            var validator = new InlineValidator<AdicionarObraBaseModel>();
            validator.RuleFor(i => i.Titulo)
                .NotEmpty().WithMessage("O título da obra deve ser informado")
                .NotNull().WithMessage("O título da obra deve ser informado")
                .MaximumLength(255).WithMessage("O título da obra não deve possuir mais que 255 caracteres");

            validator.RuleFor(i => i.Foto)
                .MaximumLength(600).WithMessage("O endereço da foto da obra não deve possuir mais que 600 caracteres");

            validator.RuleFor(i => i.Editora)
                .NotEmpty().WithMessage("A editora da obra deve ser informado")
                .NotNull().WithMessage("A editora da obra deve ser informado")
                .MaximumLength(100).WithMessage("A editora da obra não deve possuir mais que 100 caracteres");

            validator.RuleFor(i => i.Autores)
                .Must(i => i.Count > 0).WithMessage("O título deve possuir pelo menos um autor");

            var validacao = validator.Validate(this);

            Erros = validacao.Errors.Select(v => v.ErrorMessage).ToList();
        }
    }
}
