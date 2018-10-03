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
    public class ProjetoUserAppSvcGenericTests
    {
        [TestMethod()]
        public void CreateTestValido ()
        {
            var projeto = new ProjetoUser
            {
                UserId = 3,
                ProjetoId = 2,
                IsResponsavel = true,
                DateInicio = DateTime.Now.Date
            };
            var svc = new ProjetoUserAppSvcGeneric();
            var result = svc.Create(projeto);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FindByTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            throw new NotImplementedException();
        }
    }
}