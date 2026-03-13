using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class InstituicaoDTO
{
    [Required(ErrorMessage = "O título do tipo de Instituição é obrigatório")]
    public string? Endereco {  get; set; }

    [Required(ErrorMessage = "O título do tipo de usuario é obrigatório")]

    public string? Cnpj { get; set; }

    [Required(ErrorMessage = "O título do tipo de usuario é obrigatório")]

    public string? NomeFantacia { get; set; }
}
