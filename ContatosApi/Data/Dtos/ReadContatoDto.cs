namespace ContatosApi.Data.Dtos
{
    public class ReadContatoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public int Idade { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
