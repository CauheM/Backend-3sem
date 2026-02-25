using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Models;

namespace FilmesMoura.WebAPI.Repositories;

public class FilmeRepository : IFilmeRepository
{

    private readonly FilmeContext _context;

    public FilmeRepository(FilmeContext context)
    {
        _context = context;
    }

    public void AtualizarIdCorpo(Filme filmeAtualizado)
    {
        try
        {
            Filme FilmeBuscado = _context.Filmes.Find(filmeAtualizado.Idfilme)!;
            if (FilmeBuscado != null)
            {
                FilmeBuscado.Titulo = filmeAtualizado.Titulo;
                FilmeBuscado.Idgenero = filmeAtualizado.Idgenero;
            }

            _context.Filmes.Update(FilmeBuscado!);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }


    public void AtualizarIdUrl(Guid id, Filme filmeAtualizado)
    {
        try
        {
            Filme FilmeBuscado = _context.Filmes.Find(id.ToString())!;

            if (FilmeBuscado != null)
            {
                FilmeBuscado.Titulo = filmeAtualizado.Titulo;
                FilmeBuscado.Idgenero = filmeAtualizado.Idgenero;
            }

            _context.Filmes.Update(FilmeBuscado!);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Cadastrar(Filme novoFilme)
    {
        try
        {
            novoFilme.Idfilme = Guid.NewGuid().ToString();

            _context.Filmes.Add(novoFilme);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Deletar(Guid id)
    {
        try
        {
            Filme FIlmebuscado = _context.Filmes.Find(id.ToString())!;

            if (FIlmebuscado != null)
            {
                _context.Filmes.Remove(FIlmebuscado);
            }
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }   

    Filme IFilmeRepository.BuscarPorId(Guid id)
    {
        try
        {
            Filme FilmeBuscado = _context.Filmes.Find(id.ToString())!;
            return FilmeBuscado;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public List<Filme> Listar()
    {
        try
        {
            List<Filme> listarFilmes = _context.Filmes.ToList();
            return listarFilmes;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}
