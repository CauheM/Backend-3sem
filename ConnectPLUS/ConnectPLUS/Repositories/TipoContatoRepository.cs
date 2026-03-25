using ConnectPLUS.BdContextConnectPLUS;
using ConnectPLUS.Interfaces;
using ConnectPLUS.Models;

namespace ConnectPLUS.Repositories;

public class TipoContatoRepository : ITipoContratoRepository
{
    private readonly ConnectPLUSContext _context;
    public TipoContatoRepository(ConnectPLUSContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, TipoContato tipo)
    {
        var TipoContatoBuscado = _context.TipoContatos.Find(id);

        if (TipoContatoBuscado != null)
        {
            TipoContatoBuscado.Titulo = tipo.Titulo;
            _context.SaveChanges();
        }
    }

    public TipoContato BuscarPorId(Guid id)
    {
        return _context.TipoContatos.Find(id)!;
    }

    public void Cadastrar(TipoContato tipoContrato)
    {
        _context.TipoContatos.Add(tipoContrato);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var TipoContatoBuscado = _context.TipoContatos.Find(id);

        if (TipoContatoBuscado != null)
        {
          _context.TipoContatos.Remove(TipoContatoBuscado);
          _context.SaveChanges();
        }
    }

    public List<TipoContato> Listar()
    {
        return _context.TipoContatos.OrderBy(tc => tc.Titulo).ToList();
    }
}
