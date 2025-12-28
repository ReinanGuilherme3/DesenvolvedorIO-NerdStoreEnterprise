using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects;

public class Email
{
    public const int EnderecoMaxLength = 254;
    public const int EnderecoMinLength = 5;

    public string Endereco { get; private set; }

    // Construtor do EntityFramework
    protected Email() { }

    public Email(string endereco)
    {
        if (!Validar(endereco))
            throw new DomainException(message: "E-mail inválido");

        Endereco = endereco;
    }

    public static bool Validar(string email)
    {
        var regexEmail = new Regex(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.IgnoreCase
        );

        return regexEmail.IsMatch(email);
    }
}
