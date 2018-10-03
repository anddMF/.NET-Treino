using Microsoft.VisualStudio.TestTools.UnitTesting;
using BecaDotNet.Domain.Model;

namespace BecaDotNet.ApplicationService.Tests
{
    [TestClass()]
    public class UserAppServiceTests
    {
        [TestMethod()]
        public void AuthenticateUserTest_DadosValidos()
        {
            string login = "admin";
            string password = "pwd123";
            var userAppService = new UserAppService();
            var authResult = (ResultModelSingle<User>)userAppService.AuthenticateUser(login, password);
            Assert.IsNotNull(authResult);
            Assert.IsTrue(authResult.ResultObject.Id > 0);
            Assert.AreEqual(authResult.ResultObject.Login, login);
        }

        [TestMethod()]
        public void AuthenticateUserTest_DadosInvalidos()
        {
            string login = string.Empty;
            string password = string.Empty;
            var userAppService = new UserAppService();
            var authResult = (ResultModelSingle<User>)userAppService.AuthenticateUser(login, password);
            Assert.IsFalse(authResult.IsSuccess);
            Assert.IsNull(authResult.ResultObject);
        }

        [TestMethod()]
        public void GetUserTestManyDadosValidos()
        {
            var userAppService = new UserAppService();
            var filterObj = new User();
            var result = (ResultModelMany<User>)userAppService.GetUser(filterObj);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.ResultObject.Count > 0);
        }

        [TestMethod()]
        public void GetUserTestManyDadosInvalidos()
        {
            var userAppService = new UserAppService();
            var filterObj = new User { SuperiorId = 999};
            var result = (ResultModelMany<User>)userAppService.GetUser(filterObj);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.ResultObject.Count, 0);
        }

        [TestMethod()]
        public void GetUserByIdTestDadosValidos()
        {
            var userAppService = new UserAppService();
            var result = (ResultModelSingle<User>)userAppService.GetUser(1);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.ResultObject);
            Assert.AreEqual(result.ResultObject.Id, 1);
        }


        [TestMethod()]
        public void GetUserByIdTestDadosInvalidos()
        {
            var userAppService = new UserAppService();
            var result = userAppService.GetUser(-1);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result, typeof(ResultModel));
        }

        [TestMethod()]
        public void RegisterUserTestDadosValidos()
        {
            var userAppService = new UserAppService();
            var newUser = new User
            {
                Name = "Teste User Thomas App Service",
                Email = "testeuserthomas@mail.com",
                Login = "testeuserthomasappsvc",
                Password = "pwd123"
            };
            var result = (ResultModelSingle<User>)userAppService.RegisterUser(newUser);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsInstanceOfType(result.ResultObject, typeof(User));
            Assert.IsNotNull(result.ResultObject);
            Assert.IsTrue(result.ResultObject.Id > 0);
        }


        [TestMethod()]
        public void RegisterUserTestDadosInvalidos()
        {
            var userAppService = new UserAppService();
            var result = userAppService.RegisterUser(null);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod()]
        public void RemoveUserTest()
        {

        }

        [TestMethod()]
        public void UpdateUserTest()
        {
        }
    }
}
