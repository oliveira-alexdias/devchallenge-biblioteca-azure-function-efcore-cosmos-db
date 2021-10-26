using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevChallenge.Biblioteca.Data.Context
{
    public class BibliotecaContext : DbContext
    {
        private readonly IConfiguration config;

        public BibliotecaContext()
        {
        }

        public BibliotecaContext(IConfiguration config)
        {
            this.config = config;
        }

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options, IConfiguration config)
            : base(options)
        {
            this.config = config;
        }

        public DbSet<Obra> Obras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                this.config["CosmosEndpoint"],
                this.config["CosmosAccountKey"],
                this.config["CosmosDatabaseName"]);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibliotecaContext).Assembly);
        }
    }
}
