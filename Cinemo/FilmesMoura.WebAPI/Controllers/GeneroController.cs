using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Models;
using FilmesMoura.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmesMoura.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        [HttpGet("{id}")]

        public IActionResult GetByid(Guid id)
        {
            try
            {
                return Ok(_generoRepository.BuscarPorId(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_generoRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]

        public IActionResult Post (Genero novoGenero)
        {
            try
            {
                _generoRepository.Cadastrar(novoGenero);
                return StatusCode(201);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public IActionResult Put(Guid id, Genero generoAtualizado)
        {
            try
            {
                _generoRepository.AtualizarIdURL(id, generoAtualizado);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]

        public IActionResult Putbody(Genero generoAtualizado) 
        {
            try
            {
                _generoRepository.AtualizarIdCorpo(generoAtualizado);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete (Guid id)
        {
            try
            {
                _generoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
   
}
