using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BecaDotNet.Domain.Model;

namespace BecaDotNet.Repository.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        [TestMethod()]
        public void CreateTestDadosValidos()
        {
            var repository = new UserRepository();
            var newUser = new User
            {
                Name = "Teste User Thomas",
                Email = "testeuserthomas@mail.com",
                Login = "testeuserthomas",
                Password = "pwd123"
            };
            var result = repository.Create(newUser);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id > 0);
        }


        [TestMethod()]
        public void CreateTestDadosInvalidos()
        {
            var repository = new UserRepository();
            var result = repository.Create(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 0);
        }


        [TestMethod()]
        public void GetManyTest_DadosValidos()
        {
            var objRepository = new UserRepository();
            var objFilter = new User();
            var resultado = objRepository.GetMany(objFilter);
            Assert.IsNotNull(resultado);
            Assert.IsTrue((resultado.Count() > 0));
        }


        [TestMethod()]
        public void GetManyTest_DadosInvalidos()
        {
            var objRepository = new UserRepository();
            var objFilter = new User() { SuperiorId = 999 };
            var resultado = objRepository.GetMany(objFilter);
            Assert.IsNotNull(resultado);
            Assert.IsTrue((resultado.Count() == 0));
        }



        [TestMethod()]
        public void GetSingleTest_DadosValidos()
        {
            var objRepository = new UserRepository();
            var resultado = objRepository.GetSingle(1);
            Assert.IsNotNull(resultado);
            Assert.IsTrue((resultado.Id == 1));
        }

        [TestMethod()]
        public void GetSingleTest_DadosInvalidos()
        {
            var objRepository = new UserRepository();
            var resultado = objRepository.GetSingle(999);
            Assert.IsNotNull(resultado);
            Assert.IsTrue(resultado.Id == 0);
        }


        [TestMethod()]
        public void RemoveTest()
        {

        }

        [TestMethod()]
        public void UpdateTest()
        {
            var userRepository = new UserRepository();
            var oldName = "Admin";
            var newName = "Updated Name Is Test";
            var newUser = new User { Name = newName, Id = 1 };
            var result = userRepository.Update(newUser);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Name, newName);

            newUser.Name = oldName;
            result = userRepository.Update(newUser);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Name, oldName);
        }

        [TestMethod()]
        public void UpdateTestInvalidName()
        {
            var userRepository = new UserRepository();
            var newUser = new User { Name = string.Empty, Id = 1 };
            var result = userRepository.Update(newUser);
            Assert.IsNull(result);
        }   

        [TestMethod()]
        public void Authenticate_DadosValidos()
        {
            string login = "admin";
            string password = "pwd123";
            var userRepository = new UserRepository();
            var authResult = userRepository.Authenticate(login, password);
            Assert.IsNotNull(authResult);
            Assert.IsTrue(authResult.Id > 0);
            Assert.AreEqual(authResult.Login, login);
        }

        [TestMethod()]
        public void Authenticate_DadosInvalidos()
        {
            string login = string.Empty;
            string password = string.Empty;
            var userRepository = new UserRepository();
            var authResult = userRepository.Authenticate(login, password);
            Assert.IsNotNull(authResult);
            Assert.AreEqual(authResult.Id, 0);
        }
    }
}