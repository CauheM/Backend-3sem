using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private ITipoUsuario _tipoUsuario;

    public TipoUsuarioController(ITipoUsuario tipoUsuario)
    {
        _tipoUsuario = tipoUsuario;
    }

    /// <summary>
    /// Endpoint da API q faz chamadas para esse metodo de Usuarios
    /// </summary>
    /// <returns>Status code 200 e alista de tipos de evento</returns>
    [HttpGet]

    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoUsuario.Listar());
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q busca um usuario especifico
    /// </summary>
    /// <param name="id">Id do usuario buscado</param>
    /// <returns>Status code 200 e p usuario buscado</returns>
    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoUsuario.BuscarPorId(id));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q faz chamada pra cadastrar um usuario
    /// </summary>
    /// <param name="tipousuario">tipo de usuario a ser cadastrado</param>
    /// <returns>Status 201 e o tipo de usuario cadastrado</returns>
    [HttpPost]

    public IActionResult Cadastrar(TipoUsuarioDTO tipousuario)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario 
            { 
              Titulo = tipousuario.Título!
            };

            _tipoUsuario.Cadastrar(novoTipoUsuario);

            return StatusCode(201, novoTipoUsuario);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q atualiza um elemento do usuario
    /// </summary>
    /// <param name="id">id do tipo do usuario para ser atualizado</param>
    /// <param name="tipoUsuario">tipo de usuario com os dados atualizados</param>
    /// <returns>status code 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]

    public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var tipoUsuarioBuscado = new TipoUsuario
            {
                Titulo = tipoUsuario.Título!
            };
            _tipoUsuario.Atualizar(id, tipoUsuarioBuscado);
            return StatusCode(204, tipoUsuarioBuscado);  
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q deleta o tipo usuario
    /// </summary>
    /// <param name="id">id do tipo usuario para ser deletado</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _tipoUsuario.Deletar(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
