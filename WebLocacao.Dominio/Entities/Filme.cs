using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebLocacao.Dominio.Entities
{
    [Table("tbFilme", Schema = "public")]
    public class Filmes
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Nome do Filme requerido")]
        [StringLength(120, ErrorMessage = "O tamanho máximo são 120 caracteres.")]
        public string Filme { get; set; }

        [Column("Genero")]
        public string GeneroString
        {
            get { return Genero.ToString(); }
            private set { Genero = EnumExtensions.ParseEnum<Generos>(value); }
        }

        [NotMapped]
        public Generos? Genero { get; set; }

        public virtual ICollection<Locacao> Locacoes { get; set; }


        public virtual ICollection<Cliente> Clientes { get; set; }

    }
}