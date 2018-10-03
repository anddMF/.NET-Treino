namespace BecaDotNet.Domain.Model
{
    public class IdentifiedEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public IdentifiedEntity()
        {
            IsActive = true;
        }
    }
}
