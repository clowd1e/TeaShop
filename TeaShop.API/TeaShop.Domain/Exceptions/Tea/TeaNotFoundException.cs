namespace TeaShop.Domain.Exceptions.Tea
{
    public sealed class TeaNotFoundException : Exception
    {
        public TeaNotFoundException() : base("Tea not found.") { }
        public TeaNotFoundException(Guid? id)
            : base($"Tea with id \"{id}\" not found.") { }
    }
}
