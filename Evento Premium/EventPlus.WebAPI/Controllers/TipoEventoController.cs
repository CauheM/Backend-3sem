using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private readonly ITipoDeEventoRepository _tipoDeEventoRepository;
    
    //Injeção de dependência

    public TipoEventoController(ITipoDeEventoRepository eventoDeEventoRepository)
    {
        _tipoDeEventoRepository = eventoDeEventoRepository;
    }

    /// <summary>
    /// Endpoint da API q faz a chamada para o método de lista os tipos de evento
    /// </summary>
    /// <returns>Status code 200 e lista de tipos de evento</returns>
    //[Authorize]
    [HttpGet]
    
        public IActionResult Listar()
        {
            try
            {
                return Ok(_tipoDeEventoRepository.Listar());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo d buscar um tipo de evento especifico
    /// </summary>
    /// <param name="id">Id do tipo de evento buscado</param>
    /// <returns>Status code 200 e o tipo de evento buscado</returns>
    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoDeEventoRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint API q faz a chamada para o metodo de cadastrar um tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
    /// <returns>Status 201 e o tipo de evento a ser cadastrado</returns>
    [HttpPost]

    public IActionResult Cadastrar(TipoEventoDTO tipoEvento)
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
                Título = tipoEvento.Título!
            };

            _tipoDeEventoRepository.Cadastrar(novoTipoEvento);

            return StatusCode(201, novoTipoEvento);
        }
        catch (Exception e)
        {
    
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q faz chamada para o método de atualizar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo de evento a ser atualizado</param>
    /// <param name="tipoEvento">tipo de evento com os dados atualizados</param>
    /// <returns>Status 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]

    public IActionResult Atualizar(Guid id, TipoEventoDTO tipoEvento)
    {
        try
        {
            var tipoEventoAtualizado = new TipoEvento
            {
                Título = tipoEvento.Título!
            };

            _tipoDeEventoRepository.Atualizar(id, tipoEventoAtualizado);
            return StatusCode(204, tipoEvento);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q faz a chamada para o metodo deletar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo de evento a ser excluido</param>
    /// <returns>Status code 204</returns>
        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            try
            {
               _tipoDeEventoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }
        }
    
 }
