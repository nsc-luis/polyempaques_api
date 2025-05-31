using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polyempaques_API.Models
{
    [Table("modulos", Schema = "autenticacion")]
    public class Modulos
    {
        [Key]
        public int idModulo { get; set; }
        public string? nombreModulo { get; set; }
    }
}
