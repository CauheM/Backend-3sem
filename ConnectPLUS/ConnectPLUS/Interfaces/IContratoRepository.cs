using ConnectPLUS.Models;

namespace ConnectPLUS.Interfaces;

public interface IContratoRepository
{
    void Cadastrar(Contato contato);
    void Deletar(Guid id);
    List<Contato> Listar();
    Contato BuscarPorId(Guid id);
    void Atualizar(Guid id, Contato contato);

}
