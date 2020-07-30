using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Alunos")]
    public class Aluno
    {
        [Key]
        [Display(Name = "Nr. Matricula:")]
        public int NrMatricula { get; set; }

        [Required]
        [Display(Name = "Nome:")]
        [MaxLength(120, ErrorMessage = "O campo nome deve conter até 120 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "CPF:")]
        public string CPF { get; set; }

        [Display(Name = "Data de nascimento:")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "Status:")]
        public EStatus Status { get; set; }

        public virtual List<AlunoDisciplina> AlunoDisciplinas { get; set; }



    }
}
