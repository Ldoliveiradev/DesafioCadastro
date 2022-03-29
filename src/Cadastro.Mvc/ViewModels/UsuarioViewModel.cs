using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.Mvc.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [RegularExpression(@"^(\(\d{2}\)\s\d{5}\-\d{4})$", ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,10}$",
            ErrorMessage = "A senha deve conter de 6 a 10 caracteres, uma letra maiúscula, uma letra minúscula, um número e um caracter especial.")]
        public string Senha { get; set; }

        [DisplayName("Confirmar Senha")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não são iguais")]
        public string ConfirmarSenha { get; set; }
    }
}
