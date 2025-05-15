namespace RinhaDeBackEnd2023.Models.DTOs
{
    public record jsonPersonRequest(
        string nome,
        string apelido,
        string nascimento,
        string[] stack
    );
}