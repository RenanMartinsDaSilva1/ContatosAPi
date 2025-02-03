using ContatosApi.Data;
using ContatosApi.Models;

namespace ContatosApi.Negocio
{
    public class ContatoNegocio : IContatoNegocio
    {
        private ContatoContext _context;

        public ContatoNegocio(ContatoContext context)
        {
            _context = context;
        }

        public string Adcionar(Contato contato)
        {
            var mensagem = contato.ehValido();
            if (mensagem != "Contato valido")
            {
                return mensagem;
            }

            contato.DataCriacao = DateTime.UtcNow;
            contato.Ativo = true;

            _context.Contatos.Add(contato);
            _context.SaveChanges();

            return "OK";
        }

        public string Alterar(Contato contato)
        {
            var mensagem = contato.ehValido();
            if (mensagem != "Contato valido")
            {
                return mensagem;
            }

            _context.Contatos.Update(contato);
            _context.SaveChanges();

            return "OK";
        }

        public void Deletar(Contato contato)
        {
            _context.Remove(contato);
            _context.SaveChanges();
        }

        public void Inativar(Contato contato)
        {
            contato.Ativo = false;

            _context.Contatos.Update(contato);
            _context.SaveChanges();
        }

        public Contato ObterContatoPorId(int id)
        {
            return _context.Contatos.FirstOrDefault(c => c.Id == id);
        }

        public List<Contato> ObterContatos(int skip, int take)
        {
            return _context.Contatos.Skip(skip).Take(take).Where(c => c.Ativo).ToList();
        }
    }
}
