using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataNascimento { get; set; }
    }
}
