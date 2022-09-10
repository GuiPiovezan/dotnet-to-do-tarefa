using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TarefaBackEnd.Interfaces;
using TarefaBackEnd.Models;

namespace TarefaBackEnd.Controllers
{
    [ApiController]
    [Route("usuario")] 
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario, [FromServices]IUsuarioRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            repository.Create(usuario);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UsuarioLogin model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Usuario usuario = repository.Read(model.Email, model.Senha);

            if (usuario is null)
                return Unauthorized();

            usuario.Senha = "";

            return Ok(new {
                usuario,
                token = GenerateToken(usuario)
            }); ;
        }

        private string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("UmTokenMuitoGrandeEDiferenteParaNinguemDescobrir");

            var descriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(descriptior);

            return tokenHandler.WriteToken(token);
        }
    }
}
