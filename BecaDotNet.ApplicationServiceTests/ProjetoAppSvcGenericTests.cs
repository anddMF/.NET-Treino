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
    public class ProjetoAppSvcGenericTests
    {
        [TestMethod()]
        public void CreateTestValido()
        {
            var projeto = new Projeto
            {
                Nome = "NSC",
                ClienteId = 2,
                DataFinal = DateTime.Now.Date,
                DataInicio = DateTime.Now.Date
                
            };
            var svc = new ProjetoAppSvcGeneric();
            var result = svc.Create(projeto);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [TestMethod]
        public void CreateTestInvalido()
        {
            var projeto = new Projeto
            {
                Nome = null,
                
            };
            var svc = new ProjetoAppSvcGeneric();
            var result = svc.Create(projeto);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void DeleteTestValido()
        {
            var projeto = new Projeto { Id = 2 };
            var svc = new ProjetoAppSvcGeneric();
            var result = svc.Delete(projeto.Id);
            Assert.IsTrue(result);

        }

        [TestMethod()]
        public void DeleteTestInvalido()
        {
            var projeto = new Projeto { Id = 9999 };
            var svc = new ProjetoAppSvcGeneric();
            var result = svc.Delete(projeto.Id);
            Assert.IsFalse(result);

        }

        [TestMethod()]
        public void FindByTestGetProjeto()
        {
            var svc = new ProjetoAppSvcGeneric();
            var result = svc.FindBy(null);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public void FindByTestGetByName()
        {
            var svc = new ProjetoAppSvcGeneric();
            var result = svc.FindBy(new Projeto { Nome = "ITAU" });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public void GetTestValido()
        {
            var cli = new Projeto
            {
                Id = 1
            };
            var svc = new ProjetoAppSvcGeneric();
            var res = svc.Get(cli.Id);
            Assert.IsTrue(res.Id == cli.Id);
        }

        [TestMethod()]
        public void GetTestInvalido()
        {
            var cli = new Projeto
            {
                Id = 0
            };
            var svc = new ProjetoAppSvcGeneric();
            var res = svc.Get(cli.Id);
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void UpdateTestValido()
        {
            var cli = new Projeto
            {
                Id = 3,
                IsActive = true
            };
            var svc = new ProjetoAppSvcGeneric();
            var res = svc.Update(cli);
            Assert.IsNotNull(res);
            Assert.IsTrue(cli.Id > 0);
        }
    }
}