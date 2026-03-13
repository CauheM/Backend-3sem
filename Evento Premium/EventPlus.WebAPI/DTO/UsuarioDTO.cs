using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class UsuarioDTO
{
    [Required(ErrorMessage = "O título do tipo de usuario é obrigatório")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O título do tipo de usuario é obrigatório")]

    public string? Email { get; set; }

    [Required(ErrorMessage = "O título do tipo de usuario é obrigatório")]

    public string? Senha { get; set; }

    [Required(ErrorMessage = "O título do tipo de usuario é obrigatório")]

    public string? Tipo { get; set; }

    [Required(ErrorMessage = "O título do tipo de usuario é obrigatório")]

    public Guid IdTipoUsuario { get; set; }
}
