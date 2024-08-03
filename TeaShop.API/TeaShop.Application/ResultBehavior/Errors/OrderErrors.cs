namespace TeaShop.Application.ResultBehavior.Errors
{
    public static class OrderErrors
    {
        public static readonly Error OrderNotFound = new("Order.OrderNotFound", "Order not found.");
    }
}
