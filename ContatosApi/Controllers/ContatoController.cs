using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ContatosApi.Data.Dtos;
using ContatosDomain.Interfaces;
using ContatosDomain.Entidades;

namespace ContatosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        //instanciando

        private IMapper _mapper;
        private IContatoServico _contatoServico;

        public ContatoController(IContatoServico contatoServico, IMapper mapper)
        {
            _contatoServico = contatoServico;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaContato(
            [FromBody] CreateContatoDto contatoDto)
        {
            try
            {
                var contato = _mapper.Map<Contato>(contatoDto);

                var resposta = _contatoServico.Insert(contato);

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
            return _mapper.Map<List<ReadContatoDto>>(_contatoServico.GetAll(skip, take));
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaContatoPorId(int id)
        {
            var contato = _contatoServico.GetById(id);
            if (contato == null) return NotFound();
            if (contato.Ativo == false) return BadRequest("Id inativo");

            return Ok(_mapper.Map<ReadContatoDto>(contato));
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaContato(int id,
            [FromBody] UpdateContatoDto contatoDto)
        {
            var contato = _mapper.Map<Contato>(contatoDto);

            contato.Id = id;

            var resposta = _contatoServico.Update(contato);

            if (contato == null)
                return NotFound();

            if (resposta != "OK")
            {
                return BadRequest(resposta);
            }

            return Ok(_mapper.Map<ReadContatoDto>(contato));
        }

        [HttpPatch("{id}")]
        public IActionResult InativarContato(int id)
        {
            var contato = _contatoServico.GetById(id);

            if (contato == null)
                return NotFound();

            if (contato.Ativo == false)
                return BadRequest("Contato ja inativo");

            _contatoServico.Inativar(contato);

            return Ok("Objeto inativado");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaContato(int id)
        {
            var contato = _contatoServico.GetById(id);
            if (contato == null) return NotFound();

            _contatoServico.Delete(contato);

            return NoContent();
        }
    }
}
