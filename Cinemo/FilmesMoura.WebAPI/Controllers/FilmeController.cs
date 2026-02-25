using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Models;
using FilmesMoura.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmesMoura.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmeController : ControllerBase
{

    private readonly IFilmeRepository _filmeRepository;

    public FilmeController (IFilmeRepository filmeRepository)
    {
        _filmeRepository = filmeRepository;
    }

    [HttpPost]

     public IActionResult Post(Filme novofilme)
    {
        try
        {
            _filmeRepository.Cadastrar(novofilme);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_filmeRepository.Listar());
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
            _filmeRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]

    public IActionResult GetByid(Guid id)
    {
        try
        {
            return Ok(_filmeRepository.BuscarPorId(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]

    public IActionResult Putbody(Filme FilmeAtualizado)
    {
        try
        {
            _filmeRepository.AtualizarIdCorpo(FilmeAtualizado);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]

    public IActionResult Put(Guid id, Filme filmeAtualizado)
    {
        try
        {
            _filmeRepository.AtualizarIdUrl(id, filmeAtualizado);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
