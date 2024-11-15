﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeluqueriaApi.Config;

#nullable disable

namespace PeluqueriaApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PeluqueriaApi.Models.Producto.Producto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Existencias")
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Url_imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Categoria = "Herramientas",
                            Descripcion = "Tijeras de alta precisión para cortes profesionales.",
                            Existencias = 15,
                            Marca = "ProCut",
                            Nombre = "Tijeras Profesionales",
                            Precio = 2599.00m,
                            Url_imagen = "https://http2.mlstatic.com/D_NQ_NP_985742-MLA45241256474_032021-O.webp"
                        },
                        new
                        {
                            id = 2,
                            Categoria = "Estilizado",
                            Descripcion = "Cera moldeadora para un acabado natural.",
                            Existencias = 20,
                            Marca = "HairStyle",
                            Nombre = "Cera para Cabello",
                            Precio = 1099.00m,
                            Url_imagen = "https://legioners.com.ar/wp-content/uploads/2022/01/fiber-wax-100-3.jpg"
                        },
                        new
                        {
                            id = 3,
                            Categoria = "Estilizado",
                            Descripcion = "Gel fijador de alta resistencia para peinados duraderos.",
                            Existencias = 30,
                            Marca = "StrongHold",
                            Nombre = "Gel Fijador",
                            Precio = 899.00m,
                            Url_imagen = "https://simplicityar.vtexassets.com/arquivos/ids/169941-800-auto?v=638288421490970000&width=800&height=auto&aspect=true"
                        });
                });

            modelBuilder.Entity("PeluqueriaApi.Models.Rol.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Rol");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mod"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("PeluqueriaApi.Models.Rol.RolUsers", b =>
                {
                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RolId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RolUsers");
                });

            modelBuilder.Entity("PeluqueriaApi.Models.Servicio.Servicio", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("PeluqueriaApi.Models.Turno.Turno", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("FH_turno")
                        .HasColumnType("datetime2");

                    b.Property<string>("Metodo_pago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ServicioId");

                    b.HasIndex("UserId");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("PeluqueriaApi.Models.User.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("apelNom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("id");

                    b.HasIndex("userName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PeluqueriaApi.Models.Rol.RolUsers", b =>
                {
                    b.HasOne("PeluqueriaApi.Models.Rol.Rol", null)
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeluqueriaApi.Models.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PeluqueriaApi.Models.Turno.Turno", b =>
                {
                    b.HasOne("PeluqueriaApi.Models.Servicio.Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeluqueriaApi.Models.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Servicio");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}