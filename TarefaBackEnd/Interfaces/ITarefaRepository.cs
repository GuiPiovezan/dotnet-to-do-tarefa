using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarefaBackEnd.Models;

namespace TarefaBackEnd.Interfaces
{
    public interface ITarefaRepository
    {
        List<Tarefa> Read(Guid id);
        void Create(Tarefa tarefa);
        void Delete(Guid id);
        void Update(Guid id, Tarefa tarefa);
    }
}
