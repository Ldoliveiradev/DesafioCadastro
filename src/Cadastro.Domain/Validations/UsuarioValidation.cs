using Cadastro.Domain.Models;
using Cadastro.Domain.Validations.Dados;
using Cadastro.Domain.ValueObjects;
using FluentValidation;

namespace Cadastro.Domain.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido")
                .Length(3, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(u => u.Email.Endereco)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido")
                .Length(Email.EnderecoMinLength, Email.EnderecoMaxLength)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres")
                .Must(Email.IsValid).WithMessage("Email inválido");

            RuleFor(u => u.Telefone)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido")
                .Length(TelefoneValidacao.TamanhoTelefone).WithMessage("O campo {PropertyName} precisa ter 11 caracteres")
                .Must(TelefoneValidacao.Validar).WithMessage("O campo {PropertyName} é inválido");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido")
                .Length(Usuario.SenhaMinLength, Usuario.SenhaMaxLength)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
