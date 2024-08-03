namespace TeaShop.Domain.Exceptions.TeaType
{
    public sealed class TeaTypeHasNoElementsException : Exception
    {
        public TeaTypeHasNoElementsException() : base("Tea type has no elements.") { }
        public TeaTypeHasNoElementsException(Guid? id)
            : base($"Tea type with id \"{id}\" has no elements.") { }
    }
}
