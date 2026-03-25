using ConnectPLUS.DTO;
using ConnectPLUS.Interfaces;
using ConnectPLUS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPLUS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoContatoController : ControllerBase
{
    private readonly ITipoContratoRepository _TipoContatoRepository;
    public TipoContatoController(ITipoContratoRepository tipoContatoRepository)
    {
        _TipoContatoRepository = tipoContatoRepository;
    }

    [HttpGet]

    public IActionResult Listar()
    {
        try
        {
            return Ok(_TipoContatoRepository.Listar());
        }   
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_TipoContatoRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPost]

    public IActionResult Cadastrar(TipoContatoDTO tipoContatoDTO)
    {
        try
        {
            var novoTipoContato = new TipoContato
            {
                Titulo = tipoContatoDTO.Titulo!
            };

            _TipoContatoRepository.Cadastrar(novoTipoContato);

            return StatusCode(201, novoTipoContato);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]

    public IActionResult Atualizar(Guid id, TipoContatoDTO tipoContatoDTO)
    {
        try
        {
            var novoTipoContato = new TipoContato
            {
                Titulo = tipoContatoDTO.Titulo!
            };

            _TipoContatoRepository.Atualizar(id, novoTipoContato);

            return StatusCode(204, novoTipoContato);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]

    public IActionResult Delete(Guid id)
    {
        try
        {
           _TipoContatoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }
}
