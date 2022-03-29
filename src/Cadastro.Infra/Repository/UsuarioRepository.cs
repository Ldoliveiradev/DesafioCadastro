using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Models;
using Cadastro.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cadastro.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioContext _context;

        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }

        public async Task<bool> Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return await SaveChanges() > 0;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await _context.Usuarios.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            return await SaveChanges() > 0;
        }

        public async Task Remover(Guid id)
        {
            var usuario = await ObterPorId(id);

            _context.Usuarios.Remove(usuario);
            await SaveChanges();
        }

        public async Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> predicate)
        {
            return await _context.Usuarios.AsNoTracking().Where(predicate).ToListAsync();
        }

        private async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
