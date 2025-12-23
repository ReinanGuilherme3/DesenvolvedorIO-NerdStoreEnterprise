namespace NSE.Core.Utils;

public static class StringUtils
{
    public static string SomenteNumeros(this string texto)
    {
        return new string(texto.Where(char.IsDigit).ToArray());
    }
}
