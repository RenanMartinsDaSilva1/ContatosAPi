using ContatosDomain.Entidades;

namespace ContatosDomain.Interfaces
{
    public interface IContatoRepositorio : IRepositorioBase<Contato>
    {
        void Inativar(Contato contato);
    }
}
