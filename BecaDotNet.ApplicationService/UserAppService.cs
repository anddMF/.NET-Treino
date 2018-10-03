using System;
using System.Collections.Generic;
using System.Linq;
using BecaDotNet.CrossCutting;
using BecaDotNet.Domain.Model;
using BecaDotNet.Domain.Service;
using BecaDotNet.Repository;

namespace BecaDotNet.ApplicationService
{
    public class UserAppService : IUserService
    {
        public ResultModel AuthenticateUser(string login, string password)
        {
            try
            {
                var objRepository = new UserRepository();
                var resultUser = objRepository.Authenticate(login, password);
                if (resultUser.Id > 0)
                    return new ResultModelSingle<User>
                    {
                        IsSuccess = true,
                        ResultObject = resultUser
                    };

                return new ResultModelSingle<User>
                {
                    Messages = new List<string> { "Dados inválidos" }
                };
            }
            catch (Exception ex)
            {
                return ExceptionHelper.GetResultModelFromException(ex) ;
            }
        }

        public ResultModel GetUser(int userId)
        {
            try
            {
                var userRepository = new UserRepository();
                var result = userRepository.GetSingle(userId);
                return result.Id > 0 ?
                    new ResultModelSingle<User> { IsSuccess = true, ResultObject = result } :
                    new ResultModel { IsSuccess = false , Messages = new List<string> {"Usuário Inválido" }};
            }
            catch(Exception ex)
            {
                return ExceptionHelper.GetResultModelFromException(ex);
            }
        }

        public ResultModel GetUser(User filter)
        {
            try
            {
                var objRepository = new UserRepository();
                var filterResult = objRepository.GetMany(filter);
                return new ResultModelMany<User>
                {
                    IsSuccess = true,
                    ResultObject = filterResult.ToList()
                };
            }
            catch(Exception ex)
            {
                return ExceptionHelper.GetResultModelFromException(ex);
            }
        }

        public ResultModel RegisterUser(User newUser)
        {
            try
            {
                var objRepository = new UserRepository();
                var result = objRepository.Create(newUser);
                if (result.Id > 0)
                    return new ResultModelSingle<User>
                    {
                        IsSuccess = true,
                        ResultObject = result
                    };
                return new ResultModel
                {
                    IsSuccess = false,
                    Messages =
                    new List<string> { "Parametros incorretos para criar usuário" }
                };
            }
            catch(Exception ex)
            {
                return ExceptionHelper.GetResultModelFromException(ex);
            }
        }

        public ResultModel RemoveUser(int userId)
        {
            try
            {
                var rep = new UserRepository();
                var result = rep.Remove(userId);
                   return result 
                   ? new ResultModel { IsSuccess = true }
                   : new ResultModel { Messages = new List<string> { "Erro ao remover" } };
            }

            catch (Exception ex)
            {
                return ExceptionHelper.GetResultModelFromException(ex);
            }
        }

        public ResultModel UpdateUser(User updatedUser)
        {
            try
            {
                var repository = new UserRepository();
                var result = repository.Update(updatedUser);
                if (result.Id > 0)
                    return new ResultModelSingle<User> { IsSuccess = true, ResultObject = result };
                return new ResultModel { Messages = new List<string> { "Erro ao atualizar o registro" } };
            }
            catch(Exception ex)
            {
                return ExceptionHelper.GetResultModelFromException(ex);
            }
        }

    }
}
