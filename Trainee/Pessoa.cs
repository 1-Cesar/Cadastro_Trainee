using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee
{
    internal class Pessoa
    {
        //declaração de variáveis
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public String Cpf { get; set; }

        public String DataNascimento { get; set; }
        public String Idade { get; set; }
        public String Maior { get; set; }

        public String Vaga { get; set; }

        //construtor
        public Pessoa(String nome, String sobrenome, String cpf, String dataNascimento, String idade, String maior, String vaga)
        {
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Cpf = cpf;
            this.DataNascimento = dataNascimento;
            this.Idade = idade;
            this.Maior = maior;
            this.Vaga = vaga;
        }
    }
}
