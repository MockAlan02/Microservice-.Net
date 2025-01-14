using System.ComponentModel.DataAnnotations;

namespace Api.Gateway.Domain.Customer.Command
{
    public class CustomerCreateCommand
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string? Name { get; set; }
    }
}
