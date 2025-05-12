using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Models.DTOs;

namespace RinhaDeBackEnd2023.Mapper
{
    public class MapperJsonRequest
    {
        
        public static Person MapPersonFromJsonRequest(jsonPersonRequest personJsonRequest)
        {
            return new Person
            (
                personJsonRequest.apelido,
                personJsonRequest.nome,
                personJsonRequest.nascimento,
                personJsonRequest.stack
            );
        }
    }
}