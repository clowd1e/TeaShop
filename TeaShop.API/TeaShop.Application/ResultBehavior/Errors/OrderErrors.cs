namespace TeaShop.Application.ResultBehavior.Errors
{
    public static class OrderErrors
    {
        public static readonly Error OrderNotFound = new("Order.OrderNotFound", "Order not found.");
        public static readonly Error OrdersNotFound = new("Order.OrdersNotFound", "Orders not found.");
        public static readonly Error OrderStatusNotChanged = new("Order.OrderStatusNotChanged", "Order status not changed.");
    }
}
