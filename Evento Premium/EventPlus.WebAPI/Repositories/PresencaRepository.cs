using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _context;

    public PresencaRepository(EventContext context)
    {
        _context = context; 
    }

    /// <summary>
    /// método usado para atualizar a presença do usuario
    /// </summary>
    /// <param name="id">id da presença para atualizar o método</param>
    public void Atualizar(Guid id)
    {
        var presencaBuscada = _context.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;
            _context.SaveChanges();
        }    
    }

    /// <summary>
    /// Método usado para buscar presença por id
    /// </summary>
    /// <param name="id">usado pra buscar o id da presença especifica</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    /// <summary>
    /// Método q deleta a presença
    /// </summary>
    /// <param name="id">usado para deletar a presença</param>
    public void Deletar(Guid id)
    {
        var PresencaBuscada = _context.Presencas.Find(id);

        if (PresencaBuscada != null)
        {
            _context.Presencas.Remove(PresencaBuscada);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// método q cria a presença
    /// </summary>
    /// <param name="presenca">usado para registrar a presença</param>
    public void Inscrever(Presenca presenca)
    {
        _context.Presencas.Add(presenca);
        _context.SaveChanges();
    }

    /// <summary>
    /// Método q lista possiveis presenças
    /// </summary>
    /// <returns>Lista de presenças</returns>
    public List<Presenca> Listar()
    {
        return _context.Presencas.OrderBy(p => p.Situacao).ToList();
    }

    /// <summary>
    /// método usado para listar as presenças de usuario especifico
    /// </summary>
    /// <param name="IdUsuario">usado para marcar a presença do usuario</param>
    /// <returns>presenças do usuario</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}
