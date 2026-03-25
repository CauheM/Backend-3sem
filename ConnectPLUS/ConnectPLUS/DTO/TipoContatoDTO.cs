using System.ComponentModel.DataAnnotations;

namespace ConnectPLUS.DTO;

public class TipoContatoDTO
{
    [Required(ErrorMessage = "O título do tipo de Contato é obrigatório")]
    public string? Titulo { get; set; }
}
