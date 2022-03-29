using System.Text.RegularExpressions;

namespace Cadastro.Domain.Validations.Dados
{
    public class TelefoneValidacao
    {
        public const int TamanhoTelefone = 11;

        public static bool Validar(string telefone)
        {
            return TamanhoValido(Utils.ApenasNumeros(telefone));
        }

        private static bool TamanhoValido(string valor)
        {
            return valor.Length == TamanhoTelefone;
        }
    }

    public class Utils
    {
        public static string ApenasNumeros(string valor)
        {
            return Regex.Replace(valor, @"[^0-9]", "");
        }
    }
}
