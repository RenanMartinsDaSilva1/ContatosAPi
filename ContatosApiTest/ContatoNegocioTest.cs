using Moq;
using Microsoft.AspNetCore.Mvc;
using ContatosApi.Models;
using AutoMapper;
using ContatosApi.Data;
using ContatosApi.Data.Dtos;
using Microsoft.EntityFrameworkCore;
using ContatosApi.Negocio;

namespace ContatosApiTest
{
    public class ContatoNegocioTest
    {
        private readonly Mock<ContatoContext> _mockContext;
        private readonly Mock<DbSet<Contato>> _mockDbSet;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ContatoNegocio _contatoNegocio;

        public ContatoNegocioTest()
        {
            _mockDbSet = new Mock<DbSet<Contato>>();
            _mockMapper = new Mock<IMapper>();

            // Create a list containing the contato object
            var contato = new Contato
            {
                Id = 10,
                Nome = "João",
                Sobrenome = "Silva",
                DataNascimento = new DateTime(1990, 5, 20),
                Sexo = "Masculino",
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            var contatoInativo = new Contato
            {
                Id = 11,
                Nome = "Maria",
                Sobrenome = "Pereira",
                DataNascimento = new DateTime(2000, 5, 20),
                Sexo = "Feminino",
                DataCriacao = DateTime.UtcNow,
                Ativo = false
            };

            var contatos = new List<Contato> { contato, contatoInativo }.AsQueryable();

            // Setup DbSet to behave like a real DbSet using IQueryable
            _mockDbSet.As<IQueryable<Contato>>().Setup(m => m.Provider).Returns(contatos.Provider);
            _mockDbSet.As<IQueryable<Contato>>().Setup(m => m.Expression).Returns(contatos.Expression);
            _mockDbSet.As<IQueryable<Contato>>().Setup(m => m.ElementType).Returns(contatos.ElementType);
            _mockDbSet.As<IQueryable<Contato>>().Setup(m => m.GetEnumerator()).Returns(contatos.GetEnumerator());

            // Ensure SaveChanges is mocked
            _mockContext = new Mock<ContatoContext>(new DbContextOptionsBuilder<ContatoContext>().Options);
            _mockContext.Setup(c => c.Contatos).Returns(_mockDbSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);

            // Create the controller instance
            _contatoNegocio = new ContatoNegocio(_mockContext.Object);
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
            var result = _contatoNegocio.Adcionar(contato);

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
            var result = _contatoNegocio.Adcionar(contato);

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
            var result = _contatoNegocio.Adcionar(contato);

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
            var result = _contatoNegocio.Adcionar(contato);

            // Assert
            Assert.Equal("A data de nascimento é maior que hoje!", result);
        }

        [Fact]
        public void ObterContatos_VisualizarListaContatos_RetornarListaDeContatosAtivos()
        {
            // Arrange

            // Act
            var result = _contatoNegocio.ObterContatos(0, 50);

            // Assert
            Assert.Single(result);
            Assert.Equal(10, result[0].Id);
            Assert.True(result[0].Ativo);
        }

    }
}
