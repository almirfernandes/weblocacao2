using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLocacao.Dominio.Entities;

namespace WebLocacao.Infra.Context
{
    public class LocacaoContext : DbContext
    {
        public LocacaoContext():base("PgLocacao")
        {
        }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Locacao> Locacao { get; set; }

        public DbSet<Filmes> Filmes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Locacao>()
                .HasRequired<Cliente>(c => c.Clientes)
                .WithMany(e => e.Locacoes)
                .HasForeignKey(e => e.Cliente);

            modelBuilder.Entity<Locacao>()
                .HasRequired<Filmes>(c => c.Filmes)
                .WithMany(e => e.Locacoes)
                .HasForeignKey(e => e.Filme);

        }

    }
}