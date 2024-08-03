namespace TeaShop.Domain.Exceptions.Customer
{
    public sealed class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(bool many = false) 
            : base($"Customer{(many ? "s" : "")} not found.") { }
        public CustomerNotFoundException(Guid id) 
            : base($"Customer with id \"{id}\" not found.") { }
    }
}
