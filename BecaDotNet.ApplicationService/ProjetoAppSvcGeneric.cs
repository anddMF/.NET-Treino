using BecaDotNet.Domain.Model;
using BecaDotNet.Domain.Service;
using BecaDotNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecaDotNet.ApplicationService
{
    public class ProjetoAppSvcGeneric : IGenericService<Projeto>
    {
        private ProjetoRepositoryGeneric rep = new ProjetoRepositoryGeneric();

        public Projeto Create(Projeto toCreate)
        {
            try
            {
                rep.Create(toCreate);
                rep.Save();
                return toCreate;
            }
            catch (Exception wz)
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                rep.Delete(id);
                rep.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Projeto> FindBy(Projeto filter)
        {
            if (filter == null)
                filter = new Projeto();

            try
            {
                var result = rep.FindBy(
                    item => item.Nome.Contains(string.IsNullOrEmpty(filter.Nome) ? item.Nome : filter.Nome));
                return result;
            }
            catch (Exception e)
            {
                return new List<Projeto>();
            }
        }

        public Projeto Get(int id)
        {
            try
            {
                return rep.GetSingle(id);
            }
            catch
            {
                return null;
            }
        }

        public Projeto Update(Projeto toUpdate)
        {
            try
            {
                var bdProjeto = Get(toUpdate.Id);
                if (bdProjeto.Id == toUpdate.Id)
                {
                    bdProjeto.Nome = toUpdate.Nome;
                    bdProjeto.DataInicio = toUpdate.DataInicio;
                    bdProjeto.DataFinal = toUpdate.DataFinal;
                    bdProjeto.ClienteId = toUpdate.ClienteId;
                    bdProjeto.IsActive = toUpdate.IsActive;
                    rep.Update(bdProjeto);
                }
                else
                    return new Projeto();

                rep.Save();
                return toUpdate;
            }
            catch (Exception e)
            {
                return new Projeto();
            }
        }
    }
}
