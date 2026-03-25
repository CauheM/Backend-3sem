using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConnectPLUS.Models;

[Table("Contato")]
public partial class Contato
{
    [Key]
    public Guid IdContato { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    public string FormaDeContato { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    public Guid? IdTipoDeContrato { get; set; }

    [ForeignKey("IdTipoDeContrato")]
    [InverseProperty("Contatos")]
    public virtual TipoContato? IdTipoDeContratoNavigation { get; set; }
}
