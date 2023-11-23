using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private static List<Account> accounts = new List<Account>();

        // Create
        [HttpPost]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                account.Id = accounts.Count + 1;
                accounts.Add(account);
                return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
            }
            catch (Exception ex)
            {
                // log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Read
        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            try
            {
                var account = accounts.Find(a => a.Id == id);
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                // log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, [FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingAccount = accounts.Find(a => a.Id == id);
                if (existingAccount == null)
                {
                    return NotFound();
                }
                existingAccount.Name = account.Name;
                existingAccount.Balance = account.Balance;
                return NoContent();
            }
            catch (Exception ex)
            {
                // log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            try
            {
                var existingAccount = accounts.Find(a => a.Id == id);
                if (existingAccount == null)
                {
                    return NotFound();
                }
                accounts.Remove(existingAccount);
                return NoContent();
            }
            catch (Exception ex)
            {
                // log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be greater than or equal to 0.")]
        public decimal Balance { get; set; }
    }
}