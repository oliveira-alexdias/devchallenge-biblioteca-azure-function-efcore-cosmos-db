using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Entities;

namespace DevChallenge.Biblioteca.Data.Repository
{
    public interface IObraRepository
    {
        Task Adicionar(Obra item);
        Task Atualizar(Obra item);
        Task Remover(Guid id);
        Task<List<Obra>> ObterTodos();
        Task<Obra> ObterPorId(Guid id);
    }
}