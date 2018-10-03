using System.Collections.Generic;

namespace BecaDotNet.Domain.Model
{
    public class ResultModelMany<T> : ResultModel
    {
        public List<T> ResultObject { get; set; }
    }
}
