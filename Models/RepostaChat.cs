using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    [Table("RespostaChat")]
    public class RepostaChat
    {
        [Column("Id")]
        [Display(Name="Código")]
        public int Id {get;set;}

        [Column("Resposta")]
        [Display(Name="Resposta")]
        public string Resposta {get;set;}

        [Column("Mensagem")]
        [Display(Name="Mensagem")]
        public string Mensagem {get;set;}
        
    }
}