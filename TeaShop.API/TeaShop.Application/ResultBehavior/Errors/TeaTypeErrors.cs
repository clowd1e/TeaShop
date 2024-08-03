namespace TeaShop.Application.ResultBehavior.Errors
{
    public static class TeaTypeErrors
    {
        public static readonly Error TeaTypeNotFound = new("TeaType.TeaTypeNotFound", "Tea type not found.");
        public static readonly Error TeaByTeaTypeNotFound = new("TeaType.TeaByTeaTypeNotFound", "Tea by tea type not found.");
        public static readonly Error TeaTypeAlreadyExists = new("TeaType.TeaTypeAlreadyExists", "Tea type already exists.");
    }
}
