using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza o evento marcado
    /// </summary>
    /// <param name="id">id do evento q vai ser atualizado</param>
    /// <param name="evento">o evento q será atualizado</param>
    public void Atualizar(Guid id, Evento evento)
    {
        var EventoBuscado = _context.Eventos.Find(id);

        if(EventoBuscado != null)
        {
            EventoBuscado.Nome = evento.Nome;
            EventoBuscado.Descricao = evento.Descricao;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// busca por evento especifico
    /// </summary>
    /// <param name="id">id do evento desejado</param>
    /// <returns>evento buscado</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id)!;
    }

    /// <summary>
    /// cadastra evento
    /// </summary>
    /// <param name="evento">usado para cadastrar evento</param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    /// <summary>
    /// deleta evento especifico
    /// </summary>
    /// <param name="id">usado para deletar evento especifico</param>
    public void Deletar(Guid id)
    {
        var EventoBuscado = _context.Eventos.Find(id);

        if(EventoBuscado != null)
        {
            _context.Eventos.Remove(EventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Lista possiveis eventos
    /// </summary>
    /// <returns>todos os eventos registrados</returns>
    public List<Evento> Listar()
    {
        return _context.Eventos.OrderBy(evento => evento.Nome).ToList();
    }

    /// <summary>
    /// Metodo q lista os eventos q o usuario marcou presença
    /// </summary>
    /// <param name="IdUsuario">ID do usuário para filtragem</param>
    /// <returns>Uma lista de eventos filtrados</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
            .ToList();
    }

    /// <summary>
    /// Método q lista proximos eventos q irá acontecer
    /// </summary>
    /// <returns>retorna uma lista de proximos eventos</returns>
    public List<Evento> ListarProximos()
    {
        return _context.Eventos
           .Include(e => e.IdTipoEventoNavigation)
           .Include(e => e.IdInstituicaoNavigation)
           .Where(e => e.DataEvento >= DateTime.Now)
           .OrderBy(e => e.DataEvento)
           .ToList();
    }
}
