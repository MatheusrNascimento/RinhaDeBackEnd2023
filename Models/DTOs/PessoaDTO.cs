namespace RinhaDeBackEnd2023.Models.DTOs
{
    public record PessoaDTO(
        string nome,
        string apelido,
        string nascimento,
        string[] stack
    );
}