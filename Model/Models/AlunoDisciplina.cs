using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("AlunoDisciplinas")]
    public class AlunoDisciplina
    {
        [Key]
        [Display(Name = "Código:")]
        public int Id { get; set; }

        [Display(Name = "Data de matrícula:")]
        [DataType(DataType.Date)]
        public DateTime DataMatricula { get; set; }

        [Required]
        [Display(Name = "Status")]
        public EStatus Status { get; set; }

        
        public int DisciplinaId { get; set; }
        [ForeignKey("DisciplinaId")]
        public virtual Disciplina Disciplina { get; set; }
        public int NrMatricula { get; set; }
        [ForeignKey("NrMatricula")]
        public virtual Aluno Aluno { get; set; }
    }
}
