using RinhaDeBackEnd2023.Models;
using RinhaDeBackEnd2023.Models.JsonRequest;


namespace RinhaDeBackEnd2023.Mapper
{
    public class MapperJsonRequest
    {
        public static Pessoa MapPersonFromJsonRequest(PessoaJsonRequest personJsonRequest)
        {
            return new Pessoa
            (
                personJsonRequest.apelido,
                personJsonRequest.nome,
                personJsonRequest.nascimento,
                personJsonRequest.stack
            );
        }
    }
}