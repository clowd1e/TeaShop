namespace TeaShop.Domain.Exceptions.Customer
{
    public sealed class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException() : base("Cusomer already exists.") { }

        public CustomerAlreadyExistsException(string firstName, string lastName)
            : base($"Customer \"{firstName} {lastName}\" already exists.") { }
    }
}
