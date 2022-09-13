using CadastroClientes.Contracts;
using CadastroClientes.Data;
using CadastroClientes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroClientes.Repositoy
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }
        
        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> AddCliente(Cliente cliente) 
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente> UpdateClientes(Cliente clienteAlterado)
        {
            _context.Entry(clienteAlterado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return clienteAlterado;
        }
        public async Task DeleteCliente(int id)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
