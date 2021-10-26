using System.Collections.Generic;

namespace DevChallenge.Biblioteca.Models
{
    public abstract class BaseModel
    {
        protected List<string> Erros { get; set; } = new List<string>();
        
        public bool EstaValida() => Erros.Count == 0;

        public List<string> ObterErros() => Erros;
    }
}