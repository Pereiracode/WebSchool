using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class LoginViewModel
    {
        [Required]
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
    }
}