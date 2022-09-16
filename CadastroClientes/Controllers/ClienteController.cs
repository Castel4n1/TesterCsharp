using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroClientes.Data;
using CadastroClientes.Models;
using CadastroClientes.Repositoy;
using CadastroClientes.Contracts;

namespace CadastroClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository repository)
        {
            _clienteRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {

            return Ok ( await _clienteRepository.GetClientes());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            return await _clienteRepository.GetClienteById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            var clienteAlterado = await _clienteRepository.UpdateClientes(id, cliente);

            if (id != cliente.Id)
                return BadRequest();

            return Ok(clienteAlterado);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid){
                return BadRequest();
            }

            var novoCliente = await _clienteRepository.AddCliente(cliente);

            return CreatedAtAction("GetCliente", new { id = novoCliente.Id }, novoCliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _clienteRepository.DeleteCliente(id);

            return NoContent();
        }
    }
}
