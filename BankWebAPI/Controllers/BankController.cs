using BankWebAPI.Domain;
using BankWebAPI.Domain.BankAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancosController : ControllerBase
    {
        // Adicione uma lista de bancos de exemplo
        private static List<Bank> bancos = new List<Bank>
        {
            new Bank(1, "Banco do Brasil", "Rua das Bananeiras, 123", "(11) 1234-5678"),
            new Bank(2, "Caixa Econômica Federal", "Avenida dos Estados, 987", "(11) 2345-6789")
        };

        // Obtenha todos os bancos
        [HttpGet]
        public ActionResult<IEnumerable<Bank>> GetBancos()
        {
            return bancos;
        }

        // Obtenha um banco por ID
        [HttpGet("{id}")]
        public ActionResult<Bank> GetBanco(int id)
        {
            var banco = bancos.Find(b => b.Id == id);

            if (banco == null)
            {
                return NotFound();
            }

            return banco;
        }

        // Adicione um novo banco
        [HttpPost]
        public ActionResult<Bank> PostBanco(Bank banco)
        {
            bancos.Add(banco);
            return CreatedAtAction("GetBanco", new { id = banco.Id }, banco);
        }

        // Atualize um banco existente
        [HttpPut("{id}")]
        public ActionResult PutBanco(int id, Bank banco)
        {
            var bancoEncontrado = bancos.Find(b => b.Id == id);

            if (bancoEncontrado == null)
            {
                return NotFound();
            }

            bancoEncontrado.Nome = banco.Nome;
            bancoEncontrado.Endereco = banco.Endereco;
            bancoEncontrado.Telefone = banco.Telefone;

            return NoContent();
        }

        // Exclua um banco
        [HttpDelete("{id}")]
        public ActionResult DeleteBanco(int id)
        {
            var bancoEncontrado = bancos.Find(b => b.Id == id);

            if (bancoEncontrado == null)
            {
                return NotFound();
            }

            bancos.Remove(bancoEncontrado);
            return NoContent();
        }
    }
}