using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Entities.DataContext
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Domain.Usuario> usuarios { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(e => e.HasKey(e => e.Id));

            modelBuilder.Entity<Usuario>(e => e.Property(e => e.Nombre)
                .IsRequired());

            modelBuilder.Entity<Usuario>(e => e.Property(e => e.Password)
                .IsRequired());

            modelBuilder.Entity<Usuario>(e => e.Property(e => e.CorreoElectronico)
                .IsRequired());
        }
    }
}
