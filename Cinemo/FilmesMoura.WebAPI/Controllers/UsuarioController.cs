using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmesMoura.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]

    public IActionResult Post(Usuario novoUsuario)
    {
        try
        {
            _usuarioRepository.cadastrar(novoUsuario);
            return StatusCode(201, novoUsuario);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

}
