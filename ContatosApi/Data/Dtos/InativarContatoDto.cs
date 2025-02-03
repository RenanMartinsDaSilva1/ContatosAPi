using System.ComponentModel.DataAnnotations;

namespace ContatosApi.Data.Dtos
{
    public class InativarContatoDto
    {
        [Required(ErrorMessage = "O Id é obrigatório")]
        public string Id { get; } = string.Empty;
    }
}
