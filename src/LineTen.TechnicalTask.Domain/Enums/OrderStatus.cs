namespace LineTen.TechnicalTask.Domain.Enums
{
    public enum OrderStatus : int
    {
        None = 0,
        New = 1,
        PaymentReceived = 2,
        PaymentFailed = 3,
        InProgress = 4,
        Completed = 5,
        Closed = 6,
        Cancelled = 7
    }
}