using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _eventContext;

    public UsuarioRepository (EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    /// <summary>
    /// Busca um usuario por email e senha
    /// </summary>
    /// <param name="Email">Usado por buscar o email do usuario</param>
    /// <param name="Senha">Usado por buscar a senha do usuario</param>
    /// <returns></returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        //Buscar o user pelo e-mail
        var usuarioBuscado = _eventContext.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.Email == Email);

        if (usuarioBuscado != null)
        {
            //comparar o hash da senha digitada com a q esta no banco
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if(confere == true)
            {
                return usuarioBuscado;
            }
        }

        return null!;
    }

    /// <summary>
    /// Busca o usuario por ID
    /// </summary>
    /// <param name="IdUsuario"> ID do usuário a ser buscado</param>
    /// <returns>Usuario Buscado</returns>
    public Usuario BuscarPorId(Guid IdUsuario)
    {
        return _eventContext.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation)!.FirstOrDefault(usuario => usuario.IdUsuario == IdUsuario)!;
    }

    /// <summary>
    /// Cria um usuario e deixa a senha criptografada
    /// </summary>
    /// <param name="usuario">Recebe o usuario cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);
        _eventContext.Usuarios.Add(usuario);
        _eventContext.SaveChanges();
    }
}
