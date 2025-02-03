using ContatosApi.Models;

namespace ContatosApi.Negocio
{
    public interface IContatoNegocio
    {
        string Adcionar(Contato contato);
        string Alterar(Contato contato);
        List<Contato> ObterContatos(int skip, int take);
        Contato ObterContatoPorId(int id);
        void Deletar(Contato contato);
        void Inativar(Contato contato);
    }
}
