using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinhaDeBackEnd2023.Models
{
    public class Person : BaseEntity
    {
        public string apelido { get; set; } = string.Empty;
        public string nome { get; set; } = string.Empty;
        public string nascimento { get; set; } = string.Empty;
        public string[] stack { get; set; }

        public Person(string apelido, string nome, string nascimento, string[] stack = null)
        {
            this.apelido = apelido;
            this.nome = nome;
            this.nascimento = nascimento;
            this.stack = stack;
        }
    }
}