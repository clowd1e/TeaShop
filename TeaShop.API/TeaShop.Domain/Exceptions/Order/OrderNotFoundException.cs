namespace TeaShop.Domain.Exceptions.Order
{
    public sealed class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(bool many = false)
            : base($"Order{(many ? "s" : "")} not found.") { }
        public OrderNotFoundException(Guid? id)
            : base($"Order with id \"{id}\" not found.") { }
    }
}
