﻿// <auto-generated />
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("API.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("API.Models.Imc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Altura")
                        .HasColumnType("REAL");

                    b.Property<int>("AlunoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Classificacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<float>("Peso")
                        .HasColumnType("REAL");

                    b.Property<float>("ResultImc")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.ToTable("Imcs");
                });

            modelBuilder.Entity("API.Models.Imc", b =>
                {
                    b.HasOne("API.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");
                });
#pragma warning restore 612, 618
        }
    }
}
