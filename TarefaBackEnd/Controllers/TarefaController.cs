using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarefaBackEnd.Interfaces;
using TarefaBackEnd.Models;

namespace TarefaBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
        private const string id = "{id}";

        [HttpGet]
        // [AllowAnonymous] => não precisa de autorização para realizar a ação de leitura de tarefas
        public IActionResult Get([FromServices] ITarefaRepository repository)
        {
            var id = new Guid(User.Identity.Name);
            var tarefas = repository.Read(id);
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tarefa tarefa, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            tarefa.UsuarioId = new Guid(User.Identity.Name);

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
