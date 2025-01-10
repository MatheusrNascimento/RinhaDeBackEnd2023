using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RinhaDeBackEnd2023.Models;

namespace RinhaDeBackEnd2023.Mapper
{
    public class MapperJsonRequest
    {
        public static Person MapPerson(PersonJsonRequest personJsonRequest)
        {
            return new Person
            {
                apelido = personJsonRequest.apelido,
                Nome = personJsonRequest.nome,
                Nascimento = personJsonRequest.nascimento,
                Stack = personJsonRequest.stack
            };
        }
    }
}