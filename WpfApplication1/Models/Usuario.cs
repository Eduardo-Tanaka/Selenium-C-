using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApplication1.Models
{
    [Table("TB_USUARIO")]
    public class Usuario
    {
        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(7)]
        [Column("DS_MATRICULA")]
        public string Matricula { get; set; }

        [MaxLength(250)]
        [Column("DS_SENHA")]
        public string Senha { get; set; }

        [DataType(DataType.DateTime)]
        [Column("DT_CRIACAO")]
        public DateTime DataCriacao { get; set; }

        [Column("DS_NULO")]
        public int? CampoNulo { get; set; }

        [Column("DS_NAO_NULO")]
        public int CampoNaoNulo { get; set; }

        [MinLength(10)]
        [Column("DS_MINIMO")]
        public string CampoMinimo { get; set; }

        [DataType(DataType.DateTime)]
        [Column("DT_ACESSO")]
        public DateTime DataAcesso { get; set; }

        public virtual List<Tag> Tags { get; set; }
    }
}
