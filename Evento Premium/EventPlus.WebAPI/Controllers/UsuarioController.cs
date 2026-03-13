using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// Metodo usado para buscar o usuario por id
    /// </summary>
    /// <param name="id">usado pra buscar o usuario</param>
    /// <returns>StatusCode 200 e o Usuario buscado</returns>
    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Metodo q cadastra o usuario
    /// </summary>
    /// <param name="usuarioDTO">Usado para cadastrar o usuario</param>
    /// <returns>Status Code 201 e o usuario criado</returns>
    [HttpPost]

    public IActionResult Cadastrar(UsuarioDTO usuarioDTO)
    {
        try
        {
            var novoUsuario = new Usuario
            {
                Nome = usuarioDTO.Nome!,
                Senha = usuarioDTO.Senha!,
                Email = usuarioDTO.Email!,
                IdTipoUsuario = usuarioDTO.IdTipoUsuario!
            };

            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201, novoUsuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    

    
}
