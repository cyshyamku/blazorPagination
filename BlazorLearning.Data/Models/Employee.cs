using System.ComponentModel.DataAnnotations;

namespace BlazorLearning.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is mandatory")]
        [MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "EmailId is mandatory")]
        [EmailAddress]
        public string? EmailId { get; set; }
        [Required(ErrorMessage = "Address is mandatory")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "MobileNumber is mandatory")]
        [Phone]
        public string? MobileNumber { get; set; }
    }
}
