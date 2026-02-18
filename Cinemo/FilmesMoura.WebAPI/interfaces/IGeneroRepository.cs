using FilmesMoura.WebAPI.Models;

namespace FilmesMoura.WebAPI.interfaces;

public interface IGeneroRepository
{
    void Cadastrar(Genero novoGenero);
    List<Genero> Listar();
    void AtualizarIDCorpo(Genero generoatualizado);
    void AtualizarIDURL(Guid id, Genero genero);
    void Deletar(Guid id);
    Genero BuscarPorId(Guid id);    
}
