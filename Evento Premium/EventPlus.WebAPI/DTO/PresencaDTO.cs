using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class PresencaDTO
{
    [Required(ErrorMessage = "Situação é obrigatório")]
    public bool Situacao { get; set; }

    [Required(ErrorMessage = "Evento é obrigatório")]
    public Guid IdEvento { get; set; }

    [Required(ErrorMessage = "Usuario é obrigatório")]
    public Guid IdUsuario { get; set; }
}
