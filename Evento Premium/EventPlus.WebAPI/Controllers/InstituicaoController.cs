using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private readonly IInstituicao _instituicao;

    public InstituicaoController (IInstituicao instituicao) 
    {
        _instituicao = instituicao;    
    }

    /// <summary>
    /// EndPoint da API q lista as instituições
    /// </summary>
    /// <returns>Status code 200 e lista as instituições</returns>
    [HttpGet]

    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicao.Listar());
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q lista uma instituição especifica
    /// </summary>
    /// <param name="id">Id da instituição buscada</param>
    /// <returns>Status code 200 e o tipo de instituição buscada</returns>
    [HttpGet("{id}")]

    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicao.BuscarPorId(id));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q faz chamada pra cadastrar uma instituição
    /// </summary>
    /// <param name="instituicao">instituição q será cadastrada</param>
    /// <returns>Status 201 e o instituição q foi cadastrado</returns>
    [HttpPost]

    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!,
                NomeFantacia = instituicao.NomeFantacia!
            };

            _instituicao.Cadastrar(novaInstituicao);

            return StatusCode(201, novaInstituicao);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q atualiza elementos da instituição
    /// </summary>
    /// <param name="id">Id da instituição q será atualizada</param>
    /// <param name="instituicao">tipo de instituição q os dados será atualizado</param>
    /// <returns>status 204 e o tipo de instituição cadastrada</returns>
    [HttpPut("{id}")]

    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoatualizado = new Instituicao
            {
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!,
                NomeFantacia = instituicao.NomeFantacia!
            };

            _instituicao.Atualizar(id, instituicaoatualizado);
            return StatusCode(204, instituicaoatualizado);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API q faz a instituição ser deletada
    /// </summary>
    /// <param name="id">Id da instituição q irá ser deletada</param>
    /// <returns>status code 204</returns>
    [HttpDelete("{id}")]

    public IActionResult Delete(Guid id)
    {
        try
        {
            _instituicao.Deletar(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
