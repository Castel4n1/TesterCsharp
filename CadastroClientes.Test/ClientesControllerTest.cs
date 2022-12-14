using CadastroClientes.Contracts;
using CadastroClientes.Controllers;
using CadastroClientes.Repositoy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CadastroClientes.Models;
using Bogus;

namespace CadastroClientes.Test
{
    public class ClientesControllerTest
    {
        Mock<IClienteRepository> _repository;
        Cliente clienteValido;

        public ClientesControllerTest()
        {
            _repository = new Mock<IClienteRepository>();

            clienteValido = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(fake => new Cliente(
                    fake.Name.FirstName(),
                    fake.Date.Past(20),
                    fake.Internet.Email()
                    ));
        }

        [Fact]
        public async Task GetClientes_ExecutaAcao_RetornaArrayClientesAsync()
        {
            //Arrange
            ClienteController controller = new ClienteController(_repository.Object);

            //Act
            var consulta = await controller.GetClientes();

            //Assert
            Assert.IsType<OkObjectResult>(consulta.Result);
        }

        [Fact]
        public async void GetClientes_ExecutaAcao_RetornaArrayClientes()
        {
            //arrange
            ClienteController controller = new ClienteController(_repository.Object);
           
            var clientes = new List<Cliente>()
            {
                new Cliente("x", DateTime.Now, "mail"),
                new Cliente("y", DateTime.Now, "mail2")

            };

            _repository.Setup(repo => repo.GetClientes())
                .Returns(Task.FromResult(clientes));
            //Act
            var consulta = await controller.GetClientes();
  

            //Assert
            var listarClientes = Assert.IsType<List<Cliente>>((consulta.Result as OkObjectResult).Value);
            Assert.Equal(2, listarClientes.Count);

        }

        [Fact]
        public async void PostCliente_modelStateValida_ChamaRepositorioUmaVez()
        {
            //Arrange
            var controller = new ClienteController(_repository.Object);
            var cliente = clienteValido;

            _repository.Setup(repo => repo.AddCliente(cliente))
                .ReturnsAsync(cliente);
            //Act
            await controller.PostCliente(cliente);

            //Assert
            _repository.Verify(repo => repo.AddCliente(cliente), Times.Once);
        }

        [Fact]
        public async void PostCliente_modelStateValida_ChamaRepositorio()
        {
            //Arrange
            var controller = new ClienteController(_repository.Object);
            var cliente = new Cliente("", DateTime.Now, "joao@mail.com");
            controller.ModelState.AddModelError("", " ");

            _repository.Setup(repo => repo.AddCliente(cliente))
                .ReturnsAsync(cliente);
            //Act
            await controller.PostCliente(cliente);

            //Assert
            _repository.Verify(repo => repo.AddCliente(cliente), Times.Never);
        }

        [Fact]
        public async void PostCliente_ModelStateValida_RetornaCreated()
        {
            //arrange - 
            var controller = new ClienteController(_repository.Object);
            var cliente = clienteValido;

            _repository.Setup(repo => repo.AddCliente(cliente))
                .ReturnsAsync(cliente);
            //act - Aquilo que você quer testar >
            var result = await controller.PostCliente(cliente);
            //assert - Tudo que está verificando >
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
    }
}
