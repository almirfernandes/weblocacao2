using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace WebLocacao.Dominio.Entities
{
    [Table("tbCliente", Schema = "public")]
    public class Cliente
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Nome requerido")]
        [StringLength(200, ErrorMessage = "O tamanho máximo são 200 caracteres.")]
        public string  Nome { get; set; }
        [Required(ErrorMessage = "Endereço requerido")]
        [StringLength(300, ErrorMessage = "O tamanho máximo são 300 caracteres.")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "CPF obrigatório")]
        [StringLength(11, ErrorMessage = "O tamanho máximo são 11 caracteres.")]
        public string  CPF { get; set; }

    //    [Column("LocacaoId")]
    //    public int LocacaoId { get; set; }
    //    [ForeignKey("LocacaoId")]
    //public Locacao Locacao { get; set; }
        public virtual ICollection<Locacao> Locacoes { get; set; }

    //    [Column("FilmeId")]
    //    public int FilmeId { get; set; }
    //    [ForeignKey("FilmeId")]
   //     public Filmes Filme { get; set; }
        public virtual ICollection<Filmes> Filmes { get; set; }

    }
}