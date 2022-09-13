using CadastroClientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroClientes.Contracts
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetClientes();
        Task<Cliente> GetClienteById(int id);
        Task<Cliente> AddCliente(Cliente cliente);
        Task<Cliente> UpdateClientes(Cliente cliente);
        Task DeleteCliente(int id);

    }
}
