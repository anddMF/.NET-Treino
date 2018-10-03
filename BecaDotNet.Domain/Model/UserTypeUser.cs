using System;

namespace BecaDotNet.Domain.Model
{
    public class UserTypeUser : IdentifiedEntity
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
