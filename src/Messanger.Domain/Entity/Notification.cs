namespace Messanger.Domain.Entity;

public class Notification
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public NotificationType Type { get; private set; }
    public string Destination { get; private set; }
    public string Message { get; private set; }
    public NotificationStatus Status { get; private set; }
    public int RetryCount { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Notification(
        Guid userId,
        NotificationType type,
        string destination,
        string message)
    {
        UserId = userId;
        Type = type;
        Message = message;
        Destination = destination;
        Message = message;
        Status = NotificationStatus.Pending;
        RetryCount = 0;
        CreatedAt = DateTime.UtcNow;
    }
}
