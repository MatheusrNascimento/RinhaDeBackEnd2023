using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinhaDeBackEnd2023.Models
{
    public class Pessoa : BaseEntity
    {
        public string apelido { get; set; } = string.Empty;
        public string nome { get; set; } = string.Empty;
        public string nascimento { get; set; } = string.Empty;
        public string[] stack { get; set; }

        public Pessoa(string nickname, string name, string birthdate, string[] stack = null)
        {
            Id = Guid.NewGuid();
            this.apelido = nickname;
            this.nome = name;
            this.nascimento = birthdate;
            this.stack = stack;
        }
    }
}