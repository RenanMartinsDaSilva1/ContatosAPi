using ContatosDomain.Entidades;

namespace ContatosDomain.Interfaces
{
    public interface IContatoServico
    {
        string Insert(Contato obj);
        string Update(Contato obj);
        void Delete(Contato obj);
        Contato GetById(object id);
        IEnumerable<Contato> GetAll(int skip, int take);
        void Inativar(Contato contato);
    }
}
