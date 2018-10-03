namespace BecaDotNet.Domain.Model
{
    public class ResultModelSingle<T> : ResultModel
    {
        public T ResultObject { get; set; }
    }
}
