namespace DevChallenge.Biblioteca.Entities
{
    public class Autor
    {
        public Autor()
        {
        }

        public Autor(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}