using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProyectoModels.Models;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("conexion");
            optionsBuilder.UseMySQL(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.IdCard).HasName("PRIMARY");

            entity.ToTable("card");

            entity.HasIndex(e => e.IdUser, "fk_card_user");

            entity.Property(e => e.IdCard).HasColumnName("id_card");
            entity.Property(e => e.Cardtype)
                .HasMaxLength(20)
                .HasColumnName("cardtype");
            entity.Property(e => e.ExpMonth)
                .HasMaxLength(2)
                .HasColumnName("exp_month");
            entity.Property(e => e.ExpYear)
                .HasMaxLength(2)
                .HasColumnName("exp_year");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Number)
                .HasMaxLength(16)
                .HasColumnName("number");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("fk_card_user");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.IdContact).HasName("PRIMARY");

            entity.ToTable("contact");

            entity.HasIndex(e => e.IdMunicipio, "fk_contacts_municipio");

            entity.HasIndex(e => e.IdUser, "fk_contacts_user");

            entity.Property(e => e.IdContact).HasColumnName("id_contact");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(50)
                .HasColumnName("home_address");
            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(8)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.IdMunicipio)
                .HasConstraintName("fk_contacts_municipio");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("fk_contacts_user");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PRIMARY");

            entity.ToTable("departamento");

            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PRIMARY");

            entity.ToTable("municipio");

            entity.HasIndex(e => e.IdDepartamento, "fk_municipio_departamento");

            entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("fk_municipio_departamento");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.IdRole, "fk_user_rol");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("fk_user_rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
