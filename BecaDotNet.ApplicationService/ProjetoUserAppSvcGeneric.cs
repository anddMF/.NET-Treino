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
    public class ProjetoUserAppSvcGeneric : IGenericService<ProjetoUser>
    {
        private ProjetoUserRepository rep = new ProjetoUserRepository();
        public ProjetoUser Create(ProjetoUser toCreate)
        {
            rep.Create(toCreate);
            rep.Save();
            return toCreate;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjetoUser> FindBy(ProjetoUser filter)
        {
            if (filter == null)
                filter = new ProjetoUser();

            try
            {
                var result = rep.FindBy(
                    item =>item.ProjetoId > 0, 
                    a => a.Projeto, 
                    a => a.Projeto.Cliente,
                    a => a.User).ToList();
                return result;
            }
            catch (Exception e)
            {
                return new List<ProjetoUser>();
            }
        }

        public ProjetoUser Get(int id)
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

        public ProjetoUser Update(ProjetoUser toUpdate)
        {
            try
            {
                var bdProjeto = Get(toUpdate.Id);
                if (bdProjeto.Id == toUpdate.Id)
                {
                    bdProjeto.IsActive = toUpdate.IsActive;
                    bdProjeto.DataFim = toUpdate.DataFim;
                    bdProjeto.DateInicio = toUpdate.DateInicio;
                    bdProjeto.IsResponsavel = toUpdate.IsResponsavel;
                    rep.Update(bdProjeto);
                }
                else
                    return new ProjetoUser();

                rep.Save();
                return toUpdate;
            }
            catch (Exception e)
            {
                return new ProjetoUser();
            }
        }
    }
}
