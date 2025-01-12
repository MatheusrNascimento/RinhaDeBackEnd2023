using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinhaDeBackEnd2023.Models
{
    public class Person : BaseEntity
    {
        public string Nickname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Birthdate { get; set; } = string.Empty;
        public string[] Stack { get; set; }

        public Person(string nickname, string name, string birthdate, string[] stack = null)
        {
            Id = Guid.NewGuid();
            this.Nickname = nickname;
            this.Name = name;
            this.Birthdate = birthdate;
            this.Stack = stack;
        }
    }
}