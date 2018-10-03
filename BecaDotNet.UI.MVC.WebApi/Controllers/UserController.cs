
using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using BecaDotNet.UI.MVC.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BecaDotNet.UI.MVC.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET,POST,PATCH,DELETE")]
    public class UserController : ApiController
    {
        /// <summary>
        /// Retorna a mensage "Hello, {name}"
        /// </summary>
        /// <param name="name">String que é o name</param>
        /// <returns>string</returns>
        [Route("api/user/SayHello")]
        [HttpGet]
        public IHttpActionResult SayHello(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            return Ok($"Hello, {name}");
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id < 0)
                return BadRequest();

            var result = new UserAppSvcGeneric().Get(id);
            if (result.Id == 0)
                return NotFound();

            return Ok(result);

        }

        [HttpPost]
        public IHttpActionResult Post(UserViewModel model)
        {
            if (string.IsNullOrEmpty(model.login) || string.IsNullOrEmpty(model.name) || string.IsNullOrEmpty(model.email) || string.IsNullOrEmpty(model.password))
                return BadRequest();

            try
            {
                var svc = new UserAppSvcGeneric();
                var toCreate = model.GetUser();
                if (model.superiorId == null || model.superiorId <= 0)
                    toCreate.SuperiorId = 2;

                var result = svc.Create(toCreate);
                if (result.Id > 0)
                    return Ok(result);

                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var svc = new UserAppSvcGeneric();
            var user = svc.Get(id);
            if (user == null)
                return NotFound();

            var result = svc.Delete(id);
            return result ? StatusCode(HttpStatusCode.NoContent) :
                StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpPatch]
        public IHttpActionResult Patch(UserViewModel model)
        {
            if (model.id <= 0 && model.id == null)
                return BadRequest();

            var svc = new UserAppSvcGeneric();
            var toUpdate = svc.Get(model.id.Value);
            if (toUpdate == null)
                return NotFound();
            try
            {
                toUpdate.Name = model.name;
                toUpdate.Email = model.email;
                toUpdate.Login = model.login;
                toUpdate.Password = model.password;

                var result = svc.Update(toUpdate);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("api/User/list")]
        [HttpGet]
        public IHttpActionResult FindBy(string name, int? user_type_id)
        {
            try
            {
                var svc = new UserAppSvcGeneric();
                var filter = new User
                {
                    Name = name,
                    UserTypeId = user_type_id ?? 0
                };
                var result = svc.FindBy(filter);
                if (result.Count() == 0)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }
    }
}
