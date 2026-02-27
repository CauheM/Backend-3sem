using FilmesMoura.WebAPI.DTO;
using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;

namespace FilmesMoura.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]

    public IActionResult Login(LoginDTO logindto)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(logindto.Email!, logindto.Senha!);
            
            if(usuarioBuscado == null)
            {
                return NotFound("Email ou senha inválidos");
            }

            //1

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Idusuario),
              new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!)
            };

            //2

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

            //3

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4 

            var token = new JwtSecurityToken
            (
              issuer: "api_filmes",
              audience: "api_filmes",
              claims: claims, 
              expires: DateTime.Now.AddMinutes(5),
              signingCredentials: creds
            );

            //5

             return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }
}
