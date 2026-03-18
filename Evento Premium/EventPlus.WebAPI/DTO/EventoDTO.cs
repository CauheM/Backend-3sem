using EventPlus.WebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class EventoDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Nome { get; set; }   

        [Required(ErrorMessage = "Descrição é obrigatório")]

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Data é obrigatório")]
        public DateTime DataEvento { get; set; }

        [Required(ErrorMessage = "Instituicao obrigatorio")]
        public Guid IdInstituicao { get; set; }

        [Required(ErrorMessage = "Tipo evento obrigatorio")]
        public Guid IdTipoEvento { get; set; }
    }
}