using Moq;
using Microsoft.EntityFrameworkCore;
using ContatosDomain.Entidades;
using ContatosData.Contexto;
using ContatosDomain.Interfaces;
using AutoMapper;
using ContatosDomain.Servicos;

namespace ContatosApiTest
{
    public class ContatoServicoTest
    {
        private readonly Mock<MainContext> _mockContext;
        private readonly Mock<DbSet<Contato>> _mockDbSet;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IContatoRepositorio> _mockRepositorio;
        private readonly ContatoServico _contatoServico;

        public ContatoServicoTest()
        {
            _mockDbSet = new Mock<DbSet<Contato>>();
            _mockMapper = new Mock<IMapper>();

            _mockRepositorio = new Mock<IContatoRepositorio>();
            _contatoServico = new ContatoServico(_mockRepositorio.Object);

            // Ensure SaveChanges is mocked
            _mockContext = new Mock<MainContext>(new DbContextOptionsBuilder<MainContext>().Options);
            _mockContext.Setup(c => c.Contatos).Returns(_mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);
        }

        [Fact]
        public void AdicionaContato_CriarContato_RetornarOK()
        {
            // Arrange
            var contato = new Contato
            {
                Id = 1,
                Nome = "João",
                Sobrenome = "Silva",
                DataNascimento = new DateTime(1990, 5, 20),
                Sexo = "Masculino",
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            // Act
            var result = _contatoServico.Insert(contato);

            // Assert
            Assert.Equal("OK", result);
        }

        [Fact]
        public void AdicionaContato_CriarContato_RetornarIdadeZero()
        {
            // Arrange
            var contato = new Contato
            {
                Id = 1,
                Nome = "João",
                Sobrenome = "Silva",
                DataNascimento = new DateTime(2025, 1, 1),
                Sexo = "Masculino",
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            // Act
            var result = _contatoServico.Insert(contato);

            // Assert
            Assert.Equal("Contato possui zero anos!", result);
        }

        [Fact]
        public void AdicionaContato_CriarContato_RetorMenorIdade()
        {
            // Arrange
            var contato = new Contato
            {
                Id = 1,
                Nome = "João",
                Sobrenome = "Silva",
                DataNascimento = new DateTime(2018, 5, 20),
                Sexo = "Masculino",
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            // Act
            var result = _contatoServico.Insert(contato);

            // Assert
            Assert.Equal("Contato e menor de idade!", result);
        }

        [Fact]
        public void AdicionaContato_CriarContato_RetornarDataNascimentoMaiorQueHoje()
        {
            // Arrange
            var contato = new Contato
            {
                Id = 1,
                Nome = "João",
                Sobrenome = "Silva",
                DataNascimento = new DateTime(2025, 2, 4),
                Sexo = "Masculino",
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            // Act
            var result = _contatoServico.Insert(contato);

            // Assert
            Assert.Equal("A data de nascimento é maior que hoje!", result);
        }

        [Fact]
        public void ObterContatosPorId_VisualizarContato_RetornarContatoAtivo()
        {
            // Arrange
            var contatoAtivo = new Contato
            {
                Id = 1,
                Nome = "João",
                Sobrenome = "Silva",
                DataNascimento = new DateTime(1990, 5, 20),
                Sexo = "Masculino",
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            var contatoInativo = new Contato
            {
                Id = 2,
                Nome = "Maria",
                Sobrenome = "Souza",
                DataNascimento = new DateTime(1985, 8, 15),
                Sexo = "Feminino",
                DataCriacao = DateTime.UtcNow,
                Ativo = false
            };

            // Configuramos o mock para retornar essa lista quando GetAll() for chamado
            _mockRepositorio.Setup(r => r.GetById(1)).Returns(contatoAtivo);

            // Act 
            var result = _contatoServico.GetById(1);

            // Assert 
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.True(result.Ativo);
        }

        [Fact]
        public void ObterContatos_VisualizarListaContatosAtivos_RetornarListaDeContatosAtivos()
        {
            // Arrange
            var contatos = new List<Contato>
            {
                new Contato
                {
                    Id = 1,
                    Nome = "João",
                    Sobrenome = "Silva",
                    DataNascimento = new DateTime(1990, 5, 20),
                    Sexo = "Masculino",
                    DataCriacao = DateTime.UtcNow,
                    Ativo = true
                },
                new Contato
                {
                    Id = 2,
                    Nome = "Maria",
                    Sobrenome = "Souza",
                    DataNascimento = new DateTime(1985, 8, 15),
                    Sexo = "Feminino",
                    DataCriacao = DateTime.UtcNow,
                    Ativo = false
                }
            };

            // Configuramos o mock para retornar essa lista quando GetAll() for chamado
            _mockRepositorio.Setup(r => r.GetAll()).Returns(contatos);

            // Act 
            var result = _contatoServico.GetAll(0, 5).ToList();

            // Assert 
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.True(result[0].Ativo);
        }

    }
}
