using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository) 
    { 
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ComentarioEventoDTO comentarioEventoDTO)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEventoDTO.Descricao))
            {
                return BadRequest("O texto n pode ser vazio");
            }

            var request = new AnalyzeTextOptions(comentarioEventoDTO.Descricao);
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(c => c.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                IdEvento = comentarioEventoDTO.IdEvento,
                IdUsuario = comentarioEventoDTO.IdUsuario,
                Descricao = comentarioEventoDTO.Descricao,
                Exibe = !temConteudoImproprio,
                DataComentario = DateTime.Now,
            };

            _comentarioEventoRepository.Cadastrar(novoComentario);
            return StatusCode(201, novoComentario);
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
            _comentarioEventoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]

    public IActionResult Listar(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.List(IdEvento));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("ListarExibe")]

    public IActionResult ListarOExibe(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(IdEvento));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("BuscarPorIdUsuario")]

    public IActionResult BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(IdUsuario, IdEvento));    
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            throw;
        }
    }
}
