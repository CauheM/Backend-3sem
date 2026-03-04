    using FilmesMoura.WebAPI.DTO;
    using FilmesMoura.WebAPI.interfaces;
    using FilmesMoura.WebAPI.Models;
    using FilmesMoura.WebAPI.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    namespace FilmesMoura.WebAPI.Controllers;
    
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {

        private readonly IFilmeRepository _filmeRepository;

        public FilmeController (IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        [HttpPost]
        //[Consumes("multipart/form-data")]
    public async Task<IActionResult> Post([FromForm] FilmeDTO novofilme)
        {

            if (string.IsNullOrWhiteSpace(novofilme.Titulo) && novofilme.IdGenero != null)
                return BadRequest("È obrigatorio q filme tenha nome e genero");

            Filme filme = new Filme();

            if(novofilme.Imagem != null && novofilme.Imagem.Length > 0)
            {
                var extensao = Path.GetExtension(novofilme.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                //Galantia q a pasta exista

                if(!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            
                using(var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await novofilme.Imagem.CopyToAsync(stream);
                }

                filme.Imagem = nomeArquivo;
            }

            filme.Idgenero = novofilme.IdGenero.ToString();
            filme.Titulo = novofilme.Titulo!;

            try
            {
                _filmeRepository.Cadastrar(filme);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_filmeRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {

         var filmeBuscado = _filmeRepository.BuscarPorId(id);
        if (filmeBuscado == null)
            return NotFound("Filme não encontrado");

        var pastaRelativa = "wwwroot/imagens";
        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

        //Deletar o arquivo

        if (!String.IsNullOrEmpty(filmeBuscado.Imagem))
        {
            var caminho = Path.Combine(caminhoPasta, filmeBuscado.Imagem);

            if(System.IO.File.Exists(caminho))
                System.IO.File.Delete(caminho);
        }

            try
            {
                _filmeRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]

        public IActionResult GetByid(Guid id)
        {
            try
            {
                return Ok(_filmeRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]

        public IActionResult Putbody(Filme FilmeAtualizado)
        {
            try
            {
                _filmeRepository.AtualizarIdCorpo(FilmeAtualizado);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(Guid id, FilmeDTO filme)
        {

        var filmeBuscado = _filmeRepository.BuscarPorId(id);

        if (filmeBuscado == null)
            return NotFound("Filme não encontrado!");

        if(!String.IsNullOrWhiteSpace(filme.Titulo))
            filmeBuscado.Titulo = filme.Titulo;

        if(filme.IdGenero !=  null && filme.IdGenero.ToString() != filmeBuscado.Idgenero)
            filmeBuscado.Idgenero = filme.IdGenero.ToString();

        if(filme.Imagem != null && filme.Imagem.Length != 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            //Deleta o arquivo antigo oh yeah

            if (!String.IsNullOrEmpty(filmeBuscado.Imagem))
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, filmeBuscado.Imagem);
                if(System.IO.File.Exists(caminhoAntigo))
                   System.IO.File.Delete(caminhoAntigo);
            }

            //Salva a nova imagem
            var extensao = Path.GetExtension(filme.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            if(!Directory.Exists(caminhoPasta))
               Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using(var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await filme.Imagem.CopyToAsync(stream);
            }

            filmeBuscado.Imagem = nomeArquivo;

        }

            try
            {
                _filmeRepository.AtualizarIdUrl(id, filmeBuscado);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
