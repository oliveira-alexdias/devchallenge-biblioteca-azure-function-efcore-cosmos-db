using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Data.Context;
using DevChallenge.Biblioteca.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevChallenge.Biblioteca.Data.Repository
{
    public class ObraRepository : IObraRepository
    {
        private readonly BibliotecaContext context;

        public ObraRepository(IConfiguration config)
        {
            this.context = new BibliotecaContext(config);
        }

        public async Task Adicionar(Obra item)
        {
            item.CriadoEm = DateTime.Now;
            await context.Obras.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Obra item)
        {
            item.AtualizadoEm = DateTime.Now;
            context.Obras.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task Remover(Guid id)
        {
            var obra = context.Obras.FirstOrDefault(c => c.Id == id);
            
            if (obra is null) return;

            context.Obras.Remove(obra);
            await context.SaveChangesAsync();
        }

        public async Task<List<Obra>> ObterTodos()
        {
            return await context.Obras
                .OrderByDescending(i => i.CriadoEm)
                .ToListAsync();
        }
        public async Task<Obra> ObterPorId(Guid id)
        {
            return await context.Obras.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}