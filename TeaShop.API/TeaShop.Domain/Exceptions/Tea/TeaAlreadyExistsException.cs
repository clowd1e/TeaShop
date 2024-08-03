namespace TeaShop.Domain.Exceptions.Tea
{
    public sealed class TeaAlreadyExistsException : Exception
    {
        public TeaAlreadyExistsException() : base($"Tea already exists.") { }
        public TeaAlreadyExistsException(string teaName) 
            : base($"Tea \"{teaName}\" already exists.") { }
    }
}
