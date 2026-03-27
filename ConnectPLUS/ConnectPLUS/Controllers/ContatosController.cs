using ConnectPLUS.DTO;
using ConnectPLUS.Interfaces;
using ConnectPLUS.Models;
using ConnectPLUS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnectPLUS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatosController : ControllerBase
{
    private readonly IContratoRepository _contatoRepository;

    public ContatosController(IContratoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_contatoRepository.Listar());
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
            var contato = _contatoRepository.BuscarPorId(id);

            if (contato == null)
                return NotFound("Contato não encontrado");

            return Ok(contato);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromForm] ContatoDTO contatoDTO)
    {
        if (contatoDTO == null)
            return BadRequest("Dados inválidos");

        if (string.IsNullOrWhiteSpace(contatoDTO.Nome) || contatoDTO.IdTipoContato == null)
            return BadRequest("É obrigatório ter nome e tipo de contato");

        Contato contato = new Contato();

        // Upload da imagem
        if (contatoDTO.Imagem != null && contatoDTO.Imagem.Length > 0)
        {
            var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");

            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(contatoDTO.Imagem.FileName)}";
            var caminhoCompleto = Path.Combine(pasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await contatoDTO.Imagem.CopyToAsync(stream);
            }

            contato.Imagem = nomeArquivo;       
        }

        contato.Nome = contatoDTO.Nome;
        contato.FormaDeContato = contatoDTO.FormaDeContato!;
        contato.IdTipoDeContrato = contatoDTO.IdTipoContato;

        try
        {
            _contatoRepository.Cadastrar(contato);
            return StatusCode(201, contato);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromForm] ContatoDTO contatoDTO)
    {
        var contato = _contatoRepository.BuscarPorId(id);

        if (contato == null)
            return NotFound("Contato não encontrado");

        if (!string.IsNullOrWhiteSpace(contatoDTO.Nome))
            contato.Nome = contatoDTO.Nome;

        if (contatoDTO.IdTipoContato.HasValue &&
            contatoDTO.IdTipoContato.Value != contato.IdTipoDeContrato)
        {
            contato.IdTipoDeContrato = contatoDTO.IdTipoContato.Value;
        }

        // Atualizar imagem
        if (contatoDTO.Imagem != null && contatoDTO.Imagem.Length > 0)
        {
            var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");

            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            // Deleta imagem antiga
            if (!string.IsNullOrEmpty(contato.Imagem))
            {
                var caminhoAntigo = Path.Combine(pasta, contato.Imagem);
                if (System.IO.File.Exists(caminhoAntigo))
                    System.IO.File.Delete(caminhoAntigo);
            }

            var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(contatoDTO.Imagem.FileName)}";
            var caminhoNovo = Path.Combine(pasta, nomeArquivo);

            using (var stream = new FileStream(caminhoNovo, FileMode.Create))
            {
                await contatoDTO.Imagem.CopyToAsync(stream);
            }
            
            contato.FormaDeContato = contatoDTO.FormaDeContato!;
            contato.IdTipoDeContrato = contatoDTO.IdTipoContato;
            contato.Imagem = nomeArquivo;
        }

        try
        {
            _contatoRepository.Atualizar(id, contato);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _contatoRepository.Deletar(id);
            return Ok("Contato deletado com sucesso!");
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

}
