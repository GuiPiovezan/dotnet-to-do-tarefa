using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarefaBackEnd.Models;

namespace TarefaBackEnd.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Read(string email, string senha);

        void Create(Usuario usuario);
    }
}
