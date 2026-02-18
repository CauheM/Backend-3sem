using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FilmesMoura.WebAPI.Models;

[Table("Genero")]
public partial class Genero
{
    [Key]
    [Column("IDGenero")]
    [StringLength(40)]
    [Unicode(false)]
    public string Idgenero { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [InverseProperty("IdgeneroNavigation")]
    public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();
}
