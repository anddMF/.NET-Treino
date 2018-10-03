using Microsoft.VisualStudio.TestTools.UnitTesting;
using BecaDotNet.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BecaDotNet.Domain.Model;

namespace BecaDotNet.ApplicationService.Tests
{
    [TestClass()]
    public class ClienteAppSvcGenericTests
    {
        [TestMethod()]
        public void CreateTestValido()
        {
            var cliente = new Cliente
            {
                Contato = "Perdigão",
                Nome = "Gustavo",
                Cnpj = 128192182
            };
            var svc = new ClienteAppSvcGeneric();
            var result = svc.Create(cliente);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [TestMethod()]
        public void CreateTestInvalido()
        {
            var cliente = new Cliente
            {
                Contato = null,
                Nome = "Almeida",
                Cnpj = 91990192
            };
            var svc = new ClienteAppSvcGeneric();
            var result = svc.Create(cliente);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void DeleteTestValido()
        {
            var cliente = new Cliente { Id = 2 };
            var svc = new ClienteAppSvcGeneric();
            var result = svc.Delete(cliente.Id);
            Assert.IsTrue(result);
            
        }

        [TestMethod()]
        public void DeleteTestInvalido()
        {
            var cliente = new Cliente { Id = 9999 };
            var svc = new ClienteAppSvcGeneric();
            var result = svc.Delete(cliente.Id);
            Assert.IsFalse(result);

        }

        [TestMethod()]
        public void FindByTestGetCliente()
        {
            var svc = new ClienteAppSvcGeneric();
            var result = svc.FindBy(null);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public void FindByTestGetByName()
        {
            var svc = new ClienteAppSvcGeneric();
            var result = svc.FindBy(new Cliente { Nome = "almeida" });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public void GetTestValido()
        {
            var cli = new Cliente
            {
                Id = 1
            };
            var svc = new ClienteAppSvcGeneric();
            var res = svc.Get(cli.Id);
            Assert.IsTrue(res.Id == cli.Id);
        }

        [TestMethod()]
        public void GetTestInvalido()
        {
            var cli = new Cliente
            {
                Id = 0
            };
            var svc = new ClienteAppSvcGeneric();
            var res = svc.Get(cli.Id);
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void UpdateTestValido()
        {
            var cli = new Cliente
            {
                Id = 2,
                Nome = "Update",
                Contato = "Marcelo"
            };
            var svc = new ClienteAppSvcGeneric();
            var res = svc.Update(cli);
            Assert.IsNotNull(res);
            Assert.IsTrue(cli.Id > 0);
        }
    }
}