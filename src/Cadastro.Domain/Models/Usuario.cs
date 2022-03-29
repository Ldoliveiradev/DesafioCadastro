using Cadastro.Domain.Validations.Dados;
using Cadastro.Domain.ValueObjects;

namespace Cadastro.Domain.Models
{
    public class Usuario : Entity
    {
        public const int SenhaMinLength = 6;
        public const int SenhaMaxLength = 10;

        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public string Telefone { get; private set; }
        public string Senha { get; private set; }

        public Usuario(string nome, string email, string telefone, string senha)
        {
            Nome = nome;
            AtribuirEmail(email);
            AtribuirTelefone(telefone);
            Senha = senha;
        }

        protected Usuario()
        {
        }

        public void AtribuirEmail(string email)
        {
            Email = new Email(email);
        }

        public void AtribuirTelefone(string telefone)
        {
            Telefone = Utils.ApenasNumeros(telefone);
        }

        public void CriptografarSenha()
        {
            Senha = Encode(Senha);
        }

        public void DescriptografarSenha()
        {
            Senha = Decode(Senha);
        }
    }
}
