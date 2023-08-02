using System.ComponentModel.DataAnnotations;

namespace VentionTask.Data
{
    public class User
    {
        public string UserName { get; set; }
        public string UserIdentifier { get; set;}
        public int Age { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
