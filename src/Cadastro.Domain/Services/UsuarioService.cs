using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;
using Cadastro.Domain.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Domain.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Adicionar(Usuario usuario)
        {
            if (Validar(usuario)) return false;

            if (_usuarioRepository.Buscar(u => u.Email.Endereco == usuario.Email.Endereco).Result.Any())
            {
                Notificar($"O email {usuario.Email} já está sendo utilizado.");
                return false;
            }

            usuario.CriptografarSenha();

            return await _usuarioRepository.Adicionar(usuario);
        }

        public async Task<bool> Atualizar(Usuario usuario)
        {
            if (Validar(usuario)) return false;

            if (_usuarioRepository.Buscar(u => u.Email.Endereco == usuario.Email.Endereco && u.Id != usuario.Id).Result.Any())
            {
                Notificar($"Já existe um usuário com o email {usuario.Email}.");
                return false;
            }

            if (!_usuarioRepository.Buscar(u => u.Senha == usuario.Senha && u.Id == usuario.Id).Result.Any())
            {
                usuario.CriptografarSenha();
            }

            return await _usuarioRepository.Atualizar(usuario);
        }

        public async Task Remover(Guid id)
        {
            await _usuarioRepository.Remover(id);
        }

        private bool Validar(Usuario usuario)
        {
            return !ExecutarValidacao(new UsuarioValidation(), usuario);
        }

        public void Dispose()
        {
            _usuarioRepository?.Dispose();
        }
    }
}
