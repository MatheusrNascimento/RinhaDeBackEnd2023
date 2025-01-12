using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Models.JsonRequest;


namespace RinhaDeBackEnd2023.Mapper
{
    public class MapperJsonRequest
    {
        public static Person MapPersonFromJsonRequest(PersonJsonRequest personJsonRequest)
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