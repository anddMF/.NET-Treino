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
    public class ProjetoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id < 0)
                return BadRequest();

            var result = new ProjetoAppSvcGeneric().Get(id);
            if (result.Id == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Post(ProjetoViewModel model)
        {
            if (model.StartDate == DateTime.MinValue || string.IsNullOrEmpty(model.Nome))
                return BadRequest();

            try
            {
                var svc = new ProjetoAppSvcGeneric();
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

            var svc = new ProjetoAppSvcGeneric();
            var projeto = svc.Get(id);
            if (projeto == null)
                return NotFound();

            var result = svc.Delete(id);
            return result ? StatusCode(HttpStatusCode.NoContent) :
                StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpPatch]
        public IHttpActionResult Patch(ProjetoViewModel model)
        {
            if (model.Id <= 0)
                return BadRequest();

            var svc = new ProjetoAppSvcGeneric();
            var toUpdate = svc.Get(model.Id);
            if (toUpdate == null)
                return NotFound();
            try
            {
                toUpdate.Nome = model.Nome;
                toUpdate.ClienteId = model.ClienteId;
                toUpdate.IsActive = model.IsActive;
                toUpdate.DataInicio = model.StartDate;
                toUpdate.DataFinal = model.DataFinal;
                var result = svc.Update(toUpdate);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("api/Projeto/list")]
        [HttpGet]
        public IHttpActionResult FindBy(string name, int? cliente_id)
        {
            try
            {
                var svc = new ProjetoAppSvcGeneric();
                var filter = new Projeto
                {
                    Nome = name,
                    ClienteId = cliente_id?? 0
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
