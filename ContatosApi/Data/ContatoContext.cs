using ContatosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContatosApi.Data
{
    public class ContatoContext : DbContext
    {
        public ContatoContext(DbContextOptions<ContatoContext> opts)
        : base(opts)
        {
        }

        public virtual DbSet<Contato> Contatos { get; set; }
    }
}
