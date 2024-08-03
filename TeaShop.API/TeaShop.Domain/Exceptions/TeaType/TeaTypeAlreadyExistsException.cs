namespace TeaShop.Domain.Exceptions.TeaType
{
    public sealed class TeaTypeAlreadyExistsException : Exception
    {
        public TeaTypeAlreadyExistsException() : base("Tea type already exists.") { }
        public TeaTypeAlreadyExistsException(string teaTypeName) 
            : base($"Tea type with name \"{teaTypeName}\" already exists.") { }
    }
}
