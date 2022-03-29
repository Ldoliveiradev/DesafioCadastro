using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;
using Cadastro.Domain.Notificacoes;
using Cadastro.Domain.Services;
using Moq;
using Moq.AutoMock;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cadastro.Domain.Tests
{
    public class UsuarioTests
    {
        private readonly AutoMocker _mocker;
        private readonly Usuario _usuario;
        private readonly UsuarioService _usuarioService;

        public UsuarioTests()
        {
            _usuario = new Usuario("Usuario", "email@email.com", "21333334444", "Abc123456!");
            _mocker = new AutoMocker();
            _usuarioService = _mocker.CreateInstance<UsuarioService>();
        }

        [Fact(DisplayName = "Cadastrar Usuario Com Sucesso")]
        [Trait("Categoria", "Adicionar - Usuarios")]
        public async Task Usuario_CadastrarUsuario_ComSucesso()
        {
            // Arrange
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.Adicionar(_usuario)).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioService.Adicionar(_usuario);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Adicionar(_usuario), Times.Once);
            _mocker.GetMock<INotificador>().Verify(r => r.Handle(It.IsAny<Notificacao>()), Times.Never);
        }

        [Fact(DisplayName = "Cadastrar Usuario Inválido")]
        [Trait("Categoria", "Adicionar - Usuarios")]
        public async Task Usuario_CadastrarUsuario_Invalido()
        {
            // Arrange
            var usuarioInvalido = new Usuario("Us", "emailemail.c", "2133333444", "Abc123456");
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.Adicionar(_usuario)).Returns(Task.FromResult(false));

            // Act
            var result = await _usuarioService.Adicionar(usuarioInvalido);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Adicionar(usuarioInvalido), Times.Never);
            _mocker.GetMock<INotificador>().Verify(r => r.Handle(It.IsAny<Notificacao>()), Times.Exactly(4));
        }

        [Fact(DisplayName = "Editar Usuario Com Sucesso")]
        [Trait("Categoria", "Atualizar - Usuarios")]
        public async Task Usuario_EditarUsuario_ComSucesso()
        {
            // Arrange
            var usuarioAtualizado = new Usuario("UsuarioAtualizado", "EmailAtualizado@email.com", "21888889999", "Def789101#");
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.Atualizar(usuarioAtualizado)).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioService.Atualizar(usuarioAtualizado);
            var senhaAtualizada = Encoding.UTF8.GetString(Convert.FromBase64String(usuarioAtualizado.Senha));

            // Assert
            Assert.Equal("UsuarioAtualizado", usuarioAtualizado.Nome);
            Assert.Equal("EmailAtualizado@email.com", usuarioAtualizado.Email.Endereco);
            Assert.Equal("21888889999", usuarioAtualizado.Telefone);
            Assert.Equal("Def789101#", senhaAtualizada);
            Assert.True(result);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Atualizar(usuarioAtualizado), Times.Once);
            _mocker.GetMock<INotificador>().Verify(r => r.Handle(It.IsAny<Notificacao>()), Times.Never);
        }

        [Fact(DisplayName = "Editar Usuario Inválido")]
        [Trait("Categoria", "Atualizar - Usuarios")]
        public async Task Usuario_EditarUsuario_Invalido()
        {
            // Arrange
            var usuarioInvalido = new Usuario("Us", "emailemail.c", "2133333444", "Abc123456");
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.Atualizar(usuarioInvalido)).Returns(Task.FromResult(false));

            // Act
            var result = await _usuarioService.Atualizar(usuarioInvalido);

            // Assert
            Assert.Equal("Us", usuarioInvalido.Nome);
            Assert.Equal("emailemail.c", usuarioInvalido.Email.Endereco);
            Assert.Equal("2133333444", usuarioInvalido.Telefone);
            Assert.Equal("Abc123456", "Abc123456");
            Assert.False(result);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Atualizar(usuarioInvalido), Times.Never);
            _mocker.GetMock<INotificador>().Verify(r => r.Handle(It.IsAny<Notificacao>()), Times.Exactly(4));
        }
    }
}
