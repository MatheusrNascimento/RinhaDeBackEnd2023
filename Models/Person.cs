namespace RinhaDeBackEnd2023.Models
{
    public class Person : BaseEntity
    {
        public string apelido { get; set; } = string.Empty;
        public string nome { get; set; } = string.Empty;
        public string nascimento { get; set; } = string.Empty;
        public string[] stack { get; set; }

        public Person(string nickname, string name, string birthdate, string[] stack = null)
        {
            Id = Guid.NewGuid();
            this.apelido = nickname;
            this.nome = name;
            this.nascimento = birthdate;
            this.stack = stack;
        }
    }
}