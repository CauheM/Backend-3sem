using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class InstituicaoDTO
{
    [Required(ErrorMessage = "O título do tipo de evento é obrigatório")]
    public string? Endereco {  get; set; }
    public string? Cnpj { get; set; }
    public string? NomeFantacia { get; set; }
}
