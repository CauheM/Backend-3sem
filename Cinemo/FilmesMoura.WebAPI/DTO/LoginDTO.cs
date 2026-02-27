using System.ComponentModel.DataAnnotations;

namespace FilmesMoura.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do usuario é obrigatorio")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha do usuario é obrigatorio")]
    public string? Senha { get; set; }
}