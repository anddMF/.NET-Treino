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
    public class ClienteAppSvcGeneric : IGenericService<Cliente>
    {
        private ClienteRepositoryGeneric rep = new ClienteRepositoryGeneric();

        public Cliente Create(Cliente toCreate)
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

        public IEnumerable<Cliente> FindBy(Cliente filter)
        {
            if (filter == null)
                filter = new Cliente();

            try
            {
                var result = rep.FindBy(
                    item => item.Nome.Contains(string.IsNullOrEmpty(filter.Nome) ? item.Nome : filter.Nome));
                return result;
            }
            catch (Exception e)
            {
                return new List<Cliente>();
            }
        }

        public Cliente Get(int id)
        {
            try
            {
                return rep.GetSingle(id);
            }
            catch
            {
                return new Cliente();
            }
        }

        public Cliente Update(Cliente toUpdate)
        {
            try
            {
                var bdCliente = Get(toUpdate.Id);
                if (bdCliente.Id == toUpdate.Id)
                {
                    bdCliente.Nome = toUpdate.Nome;
                    bdCliente.Cnpj = toUpdate.Cnpj;
                    bdCliente.Contato = toUpdate.Contato;
                    rep.Update(bdCliente);
                }
                else
                    return new Cliente();

                rep.Save();
                return toUpdate;
            }
            catch (Exception e)
            {
                return new Cliente();
            }
        }
    }
}
