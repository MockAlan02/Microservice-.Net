using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Customer.Services.EventHandler.Command
{
    public class CustomerCreateCommand : INotification
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string? Name { get; set; }
    }
}
