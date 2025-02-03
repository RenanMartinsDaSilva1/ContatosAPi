using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ContatosApi.Data.Dtos;
using ContatosApi.Models;
using ContatosApi.Negocio;

namespace ContatosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        //instanciando
        
        private IMapper _mapper;
        private IContatoNegocio _contatoNegocio;

        public ContatoController(IContatoNegocio contatoNegocio, IMapper mapper)
        {
            _contatoNegocio = contatoNegocio;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaContato(
            [FromBody] CreateContatoDto contatoDto)
        {
            try
            {
                var contato = _mapper.Map<Contato>(contatoDto);

                var resposta = _contatoNegocio.Adcionar(contato);

                if (resposta != "OK")
                {
                    return BadRequest(resposta);
                }

                return CreatedAtAction(nameof(AdicionaContato), new { id = contato.Id }, contato);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IEnumerable<ReadContatoDto> RecuperaContato(
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadContatoDto>>(_contatoNegocio.ObterContatos(skip, take));
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaContatoPorId(int id)
        {
            var contato = _contatoNegocio.ObterContatoPorId(id);
            if (contato == null) return NotFound();
            if (contato.Ativo == false) return BadRequest("Id inativo");

            return Ok(_mapper.Map<ReadContatoDto>(contato));
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaContato(int id,
            [FromBody] UpdateContatoDto contatoDto)
        {
            var contato = _contatoNegocio.ObterContatoPorId(id);

            if (contato == null) 
                return NotFound();

            if (contato.Ativo == false) 
                return BadRequest("Id inativo");

            contato.Sobrenome = contatoDto.Sobrenome;
            contato.DataNascimento = contatoDto.DataNascimento;
            contato.Sexo = contatoDto.Sexo;
            contato.Nome = contatoDto.Nome;

            var resposta = _contatoNegocio.Alterar(contato);

            if (resposta != "OK")
            {
                return BadRequest(resposta);
            }

            return Ok(_mapper.Map<ReadContatoDto>(contato));
        }        

        [HttpPatch("{id}")]
        public IActionResult InativarContato(int id)
        {
            var contato = _contatoNegocio.ObterContatoPorId(id);
            if (contato == null) return NotFound();

            if (contato.Ativo == false) return BadRequest("Contato ja inativo");

            _contatoNegocio.Inativar(contato);

            return Ok("Objeto inativado");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaContato(int id)
        {
            var contato = _contatoNegocio.ObterContatoPorId(id);
            if (contato == null) return NotFound();

            _contatoNegocio.Deletar(contato);

            return NoContent();
        }
    }
}
