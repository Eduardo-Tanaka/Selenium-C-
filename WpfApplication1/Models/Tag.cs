using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApplication1.Models
{
    [Table("TB_TAG")]
    public class Tag
    {
        [Column("ID_TAG")]
        public int TagID { get; set; }

        [Column("NM_TAG")]
        public string Name { get; set; }

        [Column("DS_TAG")]
        public string Text { get; set; }

        [DataType(DataType.DateTime)]
        [Column("DT_TAG")]
        public DateTime Data { get; set; }

        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
