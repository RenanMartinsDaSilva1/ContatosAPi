using System.ComponentModel.DataAnnotations;

namespace ContatosApi.Data.Dtos
{
    public class UpdateContatoDto
    {
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
    }
}
