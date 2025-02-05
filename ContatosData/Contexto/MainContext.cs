using ContatosDomain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ContatosData.Contexto
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> opts)
        : base(opts)
        {
        }

        public virtual DbSet<Contato> Contatos { get; set; }
    }
}
