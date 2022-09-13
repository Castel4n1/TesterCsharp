using CadastroClientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CadastroClientes.Test
{
    
    public class ClientTest
    {
        [Fact]
        public void Idade_VinteAnosDepois_RetornaVinte()
        {
            //Arrange
            Cliente cliente = new Cliente("Jose da Silva", DateTime.Now.AddYears(-20).AddDays(-1), "jsilva@mail.com");

            //Act
            var idade = cliente.Idade();

            //Assert
            Assert.Equal(20, idade);
        }

        [Fact]
        public void Idade_VinteAnosEUmDepois_Retorna19()
        {
            //Arrange
            Cliente cliente = new Cliente("Jose", DateTime.Now.AddYears(-20).AddDays(1), "jsilva@mail.com");

            //Act
            var idade = cliente.Idade();

            //Assert
            Assert.Equal(19, idade);
        }

        [Theory]
        [InlineData("Joao","2000, 06, 15", "Joao@uol.com")]
        public void AtualizaDados_AlteraNomeEmailNascimento_RetornaAlterado(string nome, DateTime nascimento, string email)
        {
            //Arrange
            Cliente cliente = new Cliente("Homem Branco", DateTime.Now.AddYears(-20).AddDays(-1), "jsilva@mail.com");

            //Act
            cliente.AtualizaDados(nome, nascimento, email);

            //Assert
            Assert.Equal(cliente.Nome, nome);
            Assert.Equal(cliente.Nascimento, nascimento);
            Assert.Equal(cliente.Email, email);
        }
    }
}
