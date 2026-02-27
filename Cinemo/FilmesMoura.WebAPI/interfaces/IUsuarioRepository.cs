using FilmesMoura.WebAPI.Models;

namespace FilmesMoura.WebAPI.interfaces;

public interface IUsuarioRepository
{   
    void cadastrar(Usuario novoUsuario);
    Usuario BuscarporId(Guid id);
    Usuario BuscarPorEmailESenha(string email, string senha);
}
