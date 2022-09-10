using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarefaBackEnd.Interfaces;
using TarefaBackEnd.Models;

namespace TarefaBackEnd.Controllers
{
    [ApiController]
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
        private const string id = "{id}";

        [HttpGet]
        public IActionResult Get([FromServices] ITarefaRepository repository)
        {
            var tarefas = repository.Read();
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tarefa tarefa, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Create(tarefa);

            return Ok();
        }

        [HttpPut(id)]
        public IActionResult Update(string id, [FromBody] Tarefa tarefa, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Update(new Guid(id), tarefa);

            return Ok();
        }

        [HttpDelete(id)]
        public IActionResult Delete(string id, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Delete(new Guid(id));

            return Ok();
        }
    }
}
