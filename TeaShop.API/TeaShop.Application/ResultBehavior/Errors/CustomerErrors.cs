namespace TeaShop.Application.ResultBehavior.Errors
{
    public static class CustomerErrors
    {
        public static readonly Error CustomerNotFound = new("Customer.CustomerNotFound", "Customer not found.");
        public static readonly Error CustomersNotFound = new("Customer.CustomersNotFound", "Customers not found.");
        public static readonly Error CustomerAlreadyExists = new("Customer.CustomerAlreadyExists", "Customer already exists.");
    }
}
