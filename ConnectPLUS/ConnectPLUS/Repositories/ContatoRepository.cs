using ConnectPLUS.BdContextConnectPLUS;
using ConnectPLUS.Interfaces;
using ConnectPLUS.Models;

namespace ConnectPLUS.Repositories;

public class ContatoRepository : IContratoRepository
{
    private readonly ConnectPLUSContext _context;
    public ContatoRepository(ConnectPLUSContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, Contato contato)
    {
        var ContatoBuscado = _context.Contatos.Find(id);

        if (ContatoBuscado != null)
        {
            ContatoBuscado.Nome = contato.Nome;
            ContatoBuscado.FormaDeContato = contato.FormaDeContato;
            ContatoBuscado.Imagem = contato.Imagem;
            _context.SaveChanges();
        }
    }

    public Contato BuscarPorId(Guid id)
    {
        return _context.Contatos.Find(id)!;
    }

    public void Cadastrar(Contato contato)
    {
        _context.Contatos.Add(contato);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var ContatoBuscado = _context.Contatos.Find(id);

        if(ContatoBuscado != null)
        {
            _context.Contatos.Remove(ContatoBuscado);
            _context.SaveChanges();
        }
    }

    public List<Contato> Listar()
    {
        return _context.Contatos.OrderBy(tc => tc.Nome).ToList();
    }
}
