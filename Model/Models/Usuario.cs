using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Display(Name = "Login:")]
        [MinLength(6, ErrorMessage = "O campo login deve conter no mínimo 6 caracteres")]
        [MaxLength(12, ErrorMessage = "O campo login deve conter até 12 caracteres")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Senha:")]
        [MinLength(6, ErrorMessage = "O campo senha deve conter no mínimo 6 caracteres")]
        [MaxLength(12, ErrorMessage = "O campo senha deve conter até 12 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "E-mail:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
