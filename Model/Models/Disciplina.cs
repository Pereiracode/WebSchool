using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Disciplinas")]
    public class Disciplina
    {
        [Key]
        [Display(Name = "Código:")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descrição:")]
        [MaxLength(60, ErrorMessage = "O campo descrição deve conter no maximo 60 caracteres")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Status:")]
        public EStatus Status { get; set; }
    }
}
