using System;
using System.Linq;
using TarefaBackEnd.Interfaces;
using TarefaBackEnd.Models;

namespace TarefaBackEnd.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;

        public UsuarioRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario Read(string email, string senha)
        {
            return _context.Usuarios.SingleOrDefault(
                user => user.Email == email && user.Senha == senha
                );
        }
    }
}
