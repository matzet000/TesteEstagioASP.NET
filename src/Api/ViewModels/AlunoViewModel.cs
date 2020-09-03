using Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.ViewModels
{
    /// <summary>
    /// Alunos com turma
    /// </summary>
    public class AlunoViewModel
    {
        /// <summary>
        /// O Aluno
        /// </summary>
        public Aluno Aluno { get; set; }
       
        /// <summary>
        /// As turmas do Aluno
        /// </summary>
        public ICollection<Turma> Turmas { get; set; }
    }
}