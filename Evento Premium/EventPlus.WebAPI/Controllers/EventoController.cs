using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository; 
    }

    /// <summary>
    /// Endpoint da API q faz chamada pro metodo de listar eventos filtrados pelo usuario
    /// </summary>
    /// <param name="idUsuario">id do usuario para filtragem</param>
    /// <returns>Lista de eventos filtrados por usuario</returns>
    [HttpGet("Usuario/{idUsuario}")]
    public IActionResult ListarPorId(Guid idUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(idUsuario));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q faz chamada para o metodo de listar os proximos eventos
    /// </summary>
    /// <returns>Status code 200 e uma lista de proximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximos()
    {
        try
        {
            return Ok(_eventoRepository.ListarProximos());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API para cadastrar o evento
    /// </summary>
    /// <param name="evento">usado para registrar o evento</param>
    /// <returns>Status Code 201</returns>
    [HttpPost]
    public IActionResult Cadastrar(EventoDTO evento)
    {
        try
        {
            var EventoBuscado = new Evento
            {
                Nome = evento.Nome!,
                Descricao = evento.Descricao!,
                DataEvento = evento.DataEvento,
                IdInstituicao = evento.IdInstituicao,
                IdTipoEvento = evento.IdTipoEvento
            };

            _eventoRepository.Cadastrar(EventoBuscado);
            return StatusCode(201, EventoBuscado);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q lista um evento especifico
    /// </summary>
    /// <param name="id">usado para listar o evento especifico</param>
    /// <returns>Evento Listado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_eventoRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q mostra todos os eventos registrados
    /// </summary>
    /// <returns>Status Code 200 e lista todos os tipos de eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q atualiza o evento
    /// </summary>
    /// <param name="id">id usando pra encontrar o evento</param>
    /// <param name="evento">usado para atualizar o evento</param>
    /// <returns>Status Code 204</returns>
    [HttpPut("{id}")]

    public IActionResult Atualizar(Guid id, EventoDTO evento)
    {
        try
        {
            var EventoBuscado = new Evento
            {
                Nome = evento.Nome!,
                Descricao = evento.Descricao!,
                DataEvento = evento.DataEvento,
                IdInstituicao = evento.IdInstituicao,
                IdTipoEvento = evento.IdTipoEvento
            };
            _eventoRepository.Atualizar(id, EventoBuscado);
            return StatusCode(204, EventoBuscado);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q deleta o evento
    /// </summary>
    /// <param name="id">id do evento q irá ser deletado</param>
    /// <returns>Status Code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
