using System;

namespace Cloudbase.Entities
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? RegisteredOn { get; set; }
    }
}
