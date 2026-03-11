using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicao
{
    private readonly EventContext _context;

    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var InstituicaoBuscado = _context.Instituicaos.Find(id);

        if (InstituicaoBuscado != null)
        {
            InstituicaoBuscado.Cnpj = instituicao.Cnpj;
            InstituicaoBuscado.Endereco = instituicao.Endereco;
            InstituicaoBuscado.NomeFantacia = instituicao.NomeFantacia;


            _context.SaveChanges();
        }
    }

    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var InstituicaoBuscado = _context.Instituicaos.Find(id);

        if (InstituicaoBuscado != null)
        {
            _context.Instituicaos.Remove(InstituicaoBuscado);
            _context.SaveChanges();
        }
    }

    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(instituicoes => instituicoes.NomeFantacia).ToList();
    }
}
