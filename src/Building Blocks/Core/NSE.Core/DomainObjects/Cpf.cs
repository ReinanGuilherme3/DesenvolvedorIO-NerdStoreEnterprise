using NSE.Core.Utils;

namespace NSE.Core.DomainObjects;

public class Cpf
{
    public const int CpfMaxLength = 11;
    public string Numero { get; private set; }

    protected Cpf() { }

    public Cpf(string numero)
    {
        if (!Validar(numero))
            throw new DomainException("CPF inválido.");

        Numero = numero;
    }

    public static bool Validar(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            return false;

        numero = numero.SomenteNumeros();

        if (numero.Length != CpfMaxLength)
            return false;

        // Rejeita CPFs com todos os dígitos iguais (ex: 11111111111)
        if (TodosDigitosIguais(numero))
            return false;

        var cpfBase = numero.Substring(0, 9);
        var digito1 = CalcularDigito(cpfBase);
        var digito2 = CalcularDigito(cpfBase + digito1);

        return numero.EndsWith($"{digito1}{digito2}");
    }

    private static int CalcularDigito(string cpfParcial)
    {
        int soma = 0;
        int peso = cpfParcial.Length + 1;

        foreach (var c in cpfParcial)
        {
            soma += (c - '0') * peso;
            peso--;
        }

        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }

    private static bool TodosDigitosIguais(string numero)
    {
        for (int i = 1; i < numero.Length; i++)
        {
            if (numero[i] != numero[0])
                return false;
        }
        return true;
    }
}
