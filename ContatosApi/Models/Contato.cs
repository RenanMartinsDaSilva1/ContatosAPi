using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;

namespace ContatosApi.Models
{
    public class Contato
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [MaxLength(50, ErrorMessage = "O tamanho do sobrenome não pode exceder 50 caracteres")]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório")]
        public string Sexo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A idade é obrigatória")]
        [Range(1, 100, ErrorMessage = "A idade deve ser entre 1 e 100 anos")]
        public int Idade
        {
            get
            {
                DateTime hoje = DateTime.Today;
                int idade = hoje.Year - DataNascimento.Year;

                if (DataNascimento.Date > hoje.AddYears(-idade))
                {
                    idade--;
                }

                return idade;
            }
        }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public string ehValido()
        {
            
            if (EhDataNascimentoEhMaiorQueHoje())
            {
                return "A data de nascimento é maior que hoje!";
            }

            if (EhZeroAnos())
            {
                return "Contato possui zero anos!";
            }

            if (!EhMaiorDeIdade())
            {
                return "Contato e menor de idade!";
            }

            return "Contato valido";
        }

        private bool EhMaiorDeIdade()
        {
            if (Idade >= 18)
            {
                return true;
            }
            return false;
        }

        private bool EhZeroAnos()
        {
            if (Idade <= 0)
            {
                return true;
            }

            return false;
        }

        private bool EhDataNascimentoEhMaiorQueHoje()
        {
            if (DataNascimento.Date > DateTime.Today)
            {
                return true;
            }

            return false;
        }

    }
}
