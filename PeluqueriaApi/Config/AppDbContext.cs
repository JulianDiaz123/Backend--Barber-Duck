using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PeluqueriaApi.Enums;
using PeluqueriaApi.Models.Producto;
using PeluqueriaApi.Models.Rol;
using PeluqueriaApi.Models.Servicio;
using PeluqueriaApi.Models.Turno;
using PeluqueriaApi.Models.User;

namespace PeluqueriaApi.Config
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Turno> Turnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.userName).IsUnique();
            modelBuilder.Entity<Producto>().HasData(
                new Producto
                {
                    id = 1,
                    Nombre = "Tijeras Profesionales",
                    Descripcion = "Tijeras de alta precisión para cortes profesionales.",
                    Categoria = "Herramientas",
                    Precio = 2599.00m, // Precio en ARS
                    Existencias = 15,
                    Url_imagen = "https://http2.mlstatic.com/D_NQ_NP_985742-MLA45241256474_032021-O.webp",
                    Marca = "ProCut"
                },
                new Producto
                {
                    id = 2,
                    Nombre = "Cera para Cabello",
                    Descripcion = "Cera moldeadora para un acabado natural.",
                    Categoria = "Estilizado",
                    Precio = 1099.00m, // Precio en ARS
                    Existencias = 20,
                    Url_imagen = "https://legioners.com.ar/wp-content/uploads/2022/01/fiber-wax-100-3.jpg",
                    Marca = "HairStyle"
                },
                new Producto
                {
                    id = 3,
                    Nombre = "Gel Fijador",
                    Descripcion = "Gel fijador de alta resistencia para peinados duraderos.",
                    Categoria = "Estilizado",
                    Precio = 899.00m, // Precio en ARS
                    Existencias = 30,
                    Url_imagen = "https://simplicityar.vtexassets.com/arquivos/ids/169941-800-auto?v=638288421490970000&width=800&height=auto&aspect=true",
                    Marca = "StrongHold"
                }
           );

            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Name = Roles.MOD},
                new Rol { Id = 2, Name = Roles.ADMIN },
                new Rol { Id = 3, Name = Roles.USER }
            );

            modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<RolUsers>(
                l => l.HasOne<Rol>().WithMany().HasForeignKey(e => e.RolId),
                r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId)
            );
        }
    }
}
