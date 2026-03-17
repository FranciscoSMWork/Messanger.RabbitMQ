namespace Messanger.Domain.Entity;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CustomerName { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public User? User { get; private set; }


    private Order()
    {

    }

    public Order(string customerName, decimal amount, string userId)
    {
        Id = Guid.NewGuid();
        CustomerName = customerName;
        Amount = amount;
        Status = "Created";
        this.UserId = userId;
    }

    public void MarkAsProcessed()
    {
       Status = "Processed";
    }

}
