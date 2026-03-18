using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private readonly IPresencaRepository _presencaRepository;

    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Entpoint da API q lista as presenças do usuario
    /// </summary>
    /// <param name="idUsuario">usado para listar presença do usuario</param>
    /// <returns>Status Code 200 e a lista de presença do usuario</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarMinhas(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Entpoint da API q cadastra a presenca
    /// </summary>
    /// <param name="presencaDTO">usado para cadastrar a presenca</param>
    /// <returns>Status code 201 e a presenca cadastrada</returns>
    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presencaDTO)
    {
        try
        {
            var NovaPresenca = new Presenca
            {
                Situacao = presencaDTO.Situacao,
                IdEvento = presencaDTO.IdEvento,
                IdUsuario = presencaDTO.IdUsuario
            };

            _presencaRepository.Inscrever(NovaPresenca);
            return StatusCode(201, NovaPresenca);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Entpoint da API q atualiza a presença
    /// </summary>
    /// <param name="id">id usado para atualizar a presença</param>
    /// <param name="presencaDTO">usado para filtrar para atualizar a presença</param>
    /// <returns>Status Code 204 e a presença atualizado</returns>
    [HttpPut("{id}")]

    public IActionResult Atualizar(Guid id, PresencaDTO presencaDTO)
    {
        try
        {
            var NovaPresenca = new Presenca
            {
                Situacao = presencaDTO.Situacao,
                IdEvento = presencaDTO.IdEvento,
                IdUsuario = presencaDTO.IdUsuario
            };
            _presencaRepository.Atualizar(id);
            return StatusCode(204, NovaPresenca);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]

    public IActionResult Deletar(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);

            return NoContent();
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
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]

    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository   .Listar());
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }
}

