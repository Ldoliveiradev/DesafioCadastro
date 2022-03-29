using Cadastro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cadastro.Domain.Interfaces
{
    public interface IUsuarioRepository : IDisposable
    {
        Task<bool> Adicionar(Usuario usuario);
        Task<Usuario> ObterPorId(Guid id);
        Task<IEnumerable<Usuario>> ObterTodos();
        Task<bool> Atualizar(Usuario usuario);
        Task Remover(Guid id);
        Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> predicate);
    }
}
