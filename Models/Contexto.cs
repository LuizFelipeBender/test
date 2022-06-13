using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Identity.Models
{
    public class Contexto: DbContext
    {
        public Contexto( DbContextOptions<Contexto> options): base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<RepostaChat> RepostaChat {get;set;}
    }
}