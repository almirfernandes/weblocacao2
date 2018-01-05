using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebLocacao.Dominio.Entities
{
    [Table("tbLocacao", Schema = "public")]
    public class Locacao
    {
               
        [Column("tbClienteID")]
        [Display(Name = "Nome Cliente")]
        public int Cliente { get; set; }
       
        public virtual Cliente Clientes { get; set; }

        [Column("tbFilmeID")]
        [Display(Name = "Nome Filme")]
        public int Filme { get; set; }

        public virtual Filmes Filmes { get; set; }

        [Column("dataLocacao", Order = 1)]
     //   [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [Display(Name = "Data Locação")]
        
        public DateTime dataLocacao { get; set; }

        [Column("DataDevolucao", Order = 2)]
        [Display(Name = "Data Devolução")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? DataDevolucao { get; set; }

        [Key]
        public int ID { get; set; }

    }
}