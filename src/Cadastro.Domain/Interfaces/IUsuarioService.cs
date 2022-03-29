using Cadastro.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Cadastro.Domain.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task<bool> Adicionar(Usuario usuario);
        Task<bool> Atualizar(Usuario usuario);
        Task Remover(Guid id);
    }
}
