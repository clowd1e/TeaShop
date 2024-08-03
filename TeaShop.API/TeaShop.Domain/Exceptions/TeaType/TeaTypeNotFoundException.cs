namespace TeaShop.Domain.Exceptions.TeaType
{
    public sealed class TeaTypeNotFoundException : Exception
    {
        public TeaTypeNotFoundException(bool many = false) 
            : base($"Tea type{(many ? "s" : "")} not found.") { }
        public TeaTypeNotFoundException(Guid? id) 
            : base($"Tea type with id \"{id}\" not found.") { }
    }
}
