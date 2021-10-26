using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Entities;

namespace DevChallenge.Biblioteca.Models
{
    public class ObterObraModel
    {
        public ObterObraModel(Obra obra)
        {
            Id = obra.Id;
            Titulo = obra.Titulo;
            Editora = obra.Editora;
            Foto = obra.Foto;
            Autores = obra.Autores.Select(a => a.Nome).ToList();
        }

        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string Foto { get; set; }
        public IList<string> Autores { get; set; }
    }
}
