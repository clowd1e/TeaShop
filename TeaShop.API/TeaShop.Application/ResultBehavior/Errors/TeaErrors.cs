namespace TeaShop.Application.ResultBehavior.Errors
{
    public static class TeaErrors
    {
        public static readonly Error TeaNotFound = new("Tea.TeaNotFound", "Tea not found.");
        public static readonly Error TeaAlreadyExists = new("Tea.TeaAlreadyExists", "Tea already exists.");
    }
}
