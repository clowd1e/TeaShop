namespace TeaShop.Domain.Exceptions
{
    public sealed class ComparisonException : Exception
    {
        public ComparisonException() : base("Comparison delegate is not set") { }
    }
}
