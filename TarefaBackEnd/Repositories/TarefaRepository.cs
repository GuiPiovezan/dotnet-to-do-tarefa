using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarefaBackEnd.Interfaces;
using TarefaBackEnd.Models;

namespace TarefaBackEnd.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        public DataContext Context { get; set; }

        public TarefaRepository(DataContext context)
        {
            Context = context;
        }

        public void Create(Tarefa tarefa)
        {
            if (!(tarefa is null))
            {
                tarefa.Id = new Guid();
                Context.Tarefas.Add(tarefa);
                Context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var tarefa = Context.Tarefas.Find(id);
            Context.Entry(tarefa).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public List<Tarefa> Read(Guid id)
        {
            return Context.Tarefas.Where(tarefa => tarefa.UsuarioId == id).ToList();
        }

        public void Update(Guid id, Tarefa tarefa)
        {
            var _tarefa = Context.Tarefas.Find(id);

            _tarefa.Nome = tarefa.Nome;
            _tarefa.Concluida = tarefa.Concluida;

            Context.Entry(_tarefa).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
