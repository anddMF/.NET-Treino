using BecaDotNet.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BecaDotNet.ApplicationService.Tests
{
    [TestClass()]
    public class UserAppSvcGenericTests
    {
        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }


        [TestMethod()]
        public void CreateTestDadosValidos()
        {
            var usr = new User
            {
                Email = "juju@mail.com",
                IsActive = true,
                Login = "juulia",
                Name = "Júlia",
                RegisterDate = DateTime.Now,
                Password = "pwd123",
                UserTypeId = 1
            };
            var svc = new UserAppSvcGeneric();
            var res = svc.Create(usr);
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Id > 0);
        }

        [TestMethod]
        public void CreateTestDadosInvalidos()
        {
            var usr = new User
            {
                Email = null,
                IsActive = true,
                Login = "newUser",
                Name = null,
                RegisterDate = DateTime.Now,
                Password = "dwp321",
                UserTypeId = 1
            };
            var svc = new UserAppSvcGeneric();
            var res = svc.Create(usr);
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Id == 0);
        }

        [TestMethod()]
        public void DeleteTestDadosValidos()
        {
            var usr = new User
            {
                Id = 3
            };
            var svc = new UserAppSvcGeneric();
            var res = svc.Delete(usr.Id);
            Assert.IsNotNull(res);
            Assert.IsTrue(usr.IsActive == false);
        }

        [TestMethod()]
        public void DeleteTestDadosInvalidos()
        {
            var usr = new User
            {
                Email = "deletetest@mail.com",
                IsActive = false,
                Login = "newUser",
                Name = "Delete test",
                RegisterDate = DateTime.Now,
                Password = "pwd123",
                UserTypeId = 1
            };
            var svc = new UserAppSvcGeneric();
            var res = svc.Delete(usr.Id);
            Assert.IsNotNull(res);
            Assert.IsTrue(usr.IsActive == false);
        }

        [TestMethod()]
        public void FindByTest()
        {
            
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FindByTestGetUser()
        {
            var svc = new UserAppSvcGeneric();
            var result = svc.FindBy(null);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public void FindByTestGetUserByName()
        {
            var svc = new UserAppSvcGeneric();
            var result = svc.FindBy(new User { Name ="commom"});
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public void GetTestDadosValidos()
        {
            var usr = new User
            {
                Id = 1
            };
            var svc = new UserAppSvcGeneric();
            var res = svc.Get(usr.Id);
            Assert.IsTrue(res.Id == usr.Id);
            
        }

        [TestMethod()]
        public void GetTestDadosInvalidos()
        {
            var usr = new User
            {
                Id = 0
            };
            var svc = new UserAppSvcGeneric();
            var res = svc.Get(usr.Id);
            Assert.IsNull(res);
        }


        [TestMethod()]
        public void UpdateTestDadosValidos()
        {
            var usr = new User
            {
                Id=3,
                Email = "newusermail@mail.com",
                IsActive = true,
                Login = "newUser",
                Name = "New User",
                RegisterDate = DateTime.Now,
                Password = "dwp321",
                UserTypeId = 1
            };
            var svc = new UserAppSvcGeneric();
            var res = svc.Update(usr);
            Assert.IsNotNull(res);
            Assert.IsTrue(usr.Id > 0);
        }

        [TestMethod()]
        public void UpdateTestDadosInvalidos()
        {
            var usr = new User
            {
                Id = 0,
                Email = "newusermail@mail.com",
                IsActive = true,
                Login = "newUser",
                Name = "New User",
                RegisterDate = DateTime.Now,
                Password = "dwp321",
                UserTypeId = 1
            };
            var svc = new UserAppSvcGeneric();
            var res = svc.Update(usr);
            Assert.IsNotNull(res);
            Assert.IsTrue(usr.Id == 0);
        }
    }
}