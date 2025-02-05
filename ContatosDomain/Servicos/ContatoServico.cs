using ContatosDomain.Entidades;
using ContatosDomain.Interfaces;

namespace ContatosDomain.Servicos
{
    public class ContatoServico : IContatoServico
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoServico(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public void Delete(Contato obj)
        {
            _contatoRepositorio.Delete(obj);
        }

        public IEnumerable<Contato> GetAll(int skip, int take)
        {
            return _contatoRepositorio.GetAll()
                   .Where(c => c.Ativo)
                   .Skip(skip)
                   .Take(take);
        }

        public Contato GetById(object id)
        {
            var contato = _contatoRepositorio.GetById(id);

            if (!contato.Ativo)
            {
                return null;
            }

            return contato;
        }

        public void Inativar(Contato contato)
        {
            _contatoRepositorio.Inativar(contato);
        }

        public string Insert(Contato obj)
        {
            var mensagem = EhContatoValido(obj);
            if (mensagem != "Contato valido")
            {
                return mensagem;
            }

            obj.DataCriacao = DateTime.UtcNow;
            obj.Ativo = true;

            _contatoRepositorio.Insert(obj);

            return "OK";
        }

        public string Update(Contato obj)
        {
            var contato = _contatoRepositorio.GetById(obj.Id);

            if (contato == null)
                return null;

            if (!contato.Ativo)
                return "Contato Inativo";

            contato.Sobrenome = obj.Sobrenome;
            contato.DataNascimento = obj.DataNascimento;
            contato.Sexo = obj.Sexo;
            contato.Nome = obj.Nome;

            var mensagem = EhContatoValido(obj);
            if (mensagem != "Contato valido")
            {
                return mensagem;
            }

            _contatoRepositorio.Update(contato);

            return "OK";
        }

        private string EhContatoValido(Contato obj)
        {
            if (EhDataNascimentoEhMaiorQueHoje(obj))
            {
                return "A data de nascimento é maior que hoje!";
            }

            if (EhZeroAnos(obj))
            {
                return "Contato possui zero anos!";
            }

            if (!EhMaiorDeIdade(obj))
            {
                return "Contato e menor de idade!";
            }

            return "Contato valido";
        }

        private bool EhMaiorDeIdade(Contato obj)
        {
            if (obj.Idade >= 18)
            {
                return true;
            }
            return false;
        }

        private bool EhZeroAnos(Contato obj)
        {
            if (obj.Idade <= 0)
            {
                return true;
            }

            return false;
        }

        private bool EhDataNascimentoEhMaiorQueHoje(Contato obj)
        {
            if (obj.DataNascimento.Date > DateTime.Today)
            {
                return true;
            }

            return false;
        }

    }
}
