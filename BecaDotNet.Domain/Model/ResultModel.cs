using System.Collections.Generic;

namespace BecaDotNet.Domain.Model
{
    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }

        public ResultModel()
        {
            Messages = new List<string>();
        }
    }
}
