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
    public class ClienteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id < 0)
                return BadRequest();

            var result = new ClienteAppSvcGeneric().Get(id);
            if (result.Id == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Post(ClienteViewModel model)
        {
            if (string.IsNullOrEmpty(model.Nome) || model.Cnpj <= 0 || string.IsNullOrEmpty(model.Contato))
                return BadRequest();

            try
            {
                var svc = new ClienteAppSvcGeneric();
                var toCreate = model.GetEntity();
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

            var svc = new ClienteAppSvcGeneric();
            var projeto = svc.Get(id);
            if (projeto == null)
                return NotFound();

            var result = svc.Delete(id);
            return result ? StatusCode(HttpStatusCode.NoContent) :
                StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpPatch]
        public IHttpActionResult Patch(ClienteViewModel model)
        {
            if (model.Id <= 0)
                return BadRequest();

            var svc = new ClienteAppSvcGeneric();
            var toUpdate = svc.Get(model.Id);
            if (toUpdate == null)
                return NotFound();
            try
            {
                toUpdate.Cnpj = model.Cnpj;
                toUpdate.Nome = model.Nome;
                toUpdate.Id = model.Id;
                toUpdate.Contato = model.Contato;

                var result = svc.Update(toUpdate);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("api/Cliente/list")]
        [HttpGet]
        public IHttpActionResult FindBy(string name, int? id)
        {
            try
            {
                var svc = new ClienteAppSvcGeneric();
                var filter = new Cliente
                {
                    Nome = name,
                    Id = id ?? 0
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
