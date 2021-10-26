using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevChallenge.Biblioteca.Entities
{
    public class Obra
    {
        public Obra()
        {
        }

        public Obra(string titulo, string editora, string foto, IList<string> autores)
        {
            Titulo = titulo;
            Editora = editora;
            Foto = foto;
            Autores = autores.Select(a => new Autor(a)).ToList();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string Foto { get; set; }
        public IList<Autor> Autores { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
