using System;
namespace API.Models
{
    public class Imc
    {
        public int Id { get; set; }
        public float ResultImc { get; set; }
        
        public float Altura { get; set; }

        public float Peso { get; set; }

        public string? Classificacao { get; set; }

        public string? DataCriacao { get; set; }

        public Aluno? Aluno { get; set; }

        public int AlunoId { get; set; }
    }
}