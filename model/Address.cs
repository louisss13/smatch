using System.ComponentModel.DataAnnotations;

namespace model
{
    public class Address
    {
        public long Id { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        public string Box { get; set; }
        

    }
}