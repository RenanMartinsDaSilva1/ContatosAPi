using ContatosDomain.Interfaces;
using ContatosDomain.Entidades;
using ContatosData.Contexto;

namespace ContatosData.Repositorio
{
    public class ContatoRepositorio : RepositorioBase<Contato>, IContatoRepositorio
    {
        public ContatoRepositorio(MainContext context) : base(context)
        {
                
        }

        public void Inativar(Contato contato)
        {
            contato.Ativo = false;

            Update(contato);
        }
    }
}
