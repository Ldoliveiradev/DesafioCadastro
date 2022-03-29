using System.Text.RegularExpressions;

namespace Cadastro.Domain.ValueObjects
{
    public class Email
    {
        public const int EnderecoMaxLength = 30;
        public const int EnderecoMinLength = 10;

        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            Endereco = endereco;
        }

        public static bool IsValid(string email)
        {
            var regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regexEmail.IsMatch(email);
        }
    }
}
