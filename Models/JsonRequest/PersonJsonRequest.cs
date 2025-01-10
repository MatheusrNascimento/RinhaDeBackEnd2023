using System.ComponentModel.DataAnnotations;

namespace RinhaDeBackEnd2023.Models.JsonRequest
{
    public class PersonJsonRequest
    {
        public string apelido { get; set; }
        public string nome { get; set; }
        public string nascimento { get; set; }
        public string[] stack { get; set; }
    }
}