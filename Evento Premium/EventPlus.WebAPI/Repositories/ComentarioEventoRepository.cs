using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _eventContext;

    public ComentarioEventoRepository(EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        return _eventContext.ComentarioEventos.FirstOrDefault(cm => cm.IdUsuario == IdUsuario && cm.IdEvento == IdEvento)!;
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _eventContext.ComentarioEventos.Add(comentarioEvento);
        _eventContext.SaveChanges();
    }

    public void Deletar(Guid IdComentarioEvento)
    {
        var ComentarioBuscado = _eventContext.ComentarioEventos.Find(IdComentarioEvento);

        if (ComentarioBuscado != null)
        {
            _eventContext.ComentarioEventos.Remove(ComentarioBuscado);
            _eventContext.SaveChanges();
        }
    }

    public List<ComentarioEvento> List(Guid IdEvento)
    {
        return _eventContext.ComentarioEventos.OrderBy(cm => cm.IdEvento).ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _eventContext.ComentarioEventos.Where(cm => cm.IdEvento == IdEvento && cm.Exibe).ToList()!;
    }

}
