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
            if (tarefa.Equals(null))
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

        public List<Tarefa> Read()
        {
            return Context.Tarefas.ToList();
        }

        public void Update(Tarefa tarefa)
        {
            var _tarefa = Context.Tarefas.Find(tarefa.Id);
            Context.Entry(_tarefa).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
