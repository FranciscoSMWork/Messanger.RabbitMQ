using System.ComponentModel.DataAnnotations;

namespace Messanger.API.Dtos.Orders
{
    public class CreateOrderRequest
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
