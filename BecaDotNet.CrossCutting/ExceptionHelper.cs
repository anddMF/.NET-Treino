using BecaDotNet.Domain.Model;
using System;

namespace BecaDotNet.CrossCutting
{
    public static class ExceptionHelper
    {
        public static ResultModel GetResultModelFromException(Exception ex)
        {
            var result = new ResultModel();
            result.Messages.Add(ex.Message);
            if (ex.InnerException != null)
                result.Messages.Add(ex.InnerException.Message);

            return result;
        }
    }
}
