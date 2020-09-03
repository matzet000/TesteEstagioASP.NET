using Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.ViewModels
{
    public class AlunoViewModel
    {
        public Aluno Aluno { get; set; }
       
        public ICollection<Turma> Turmas { get; set; }
    }
}