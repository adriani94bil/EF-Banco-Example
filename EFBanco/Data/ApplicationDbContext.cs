using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFBanco.Models;

namespace EFBanco.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EFBanco.Models.Banco> Banco { get; set; }

        public DbSet<EFBanco.Models.Sucursal> Sucursal { get; set; }

        public DbSet<EFBanco.Models.Cliente> Cliente { get; set; }
    }
}
