using ConnectPLUS.Models;

namespace ConnectPLUS.Interfaces;

public interface ITipoContratoRepository
{
    void Cadastrar(TipoContato tipoContrato);
    void Deletar(Guid id);
    List<TipoContato> Listar();
    TipoContato BuscarPorId(Guid id);
    void Atualizar(Guid id, TipoContato tipo);
}
