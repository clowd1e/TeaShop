namespace TeaShop.Application.ResultBehavior.Errors.Identity
{
    public sealed class RegisterErrors
    {
        public static Error EmailAlreadyExists => new("Register.EmailAlreadyExists", "Email already exists.");
        public static Error UserCreationFailed => new("Register.UserCreationFailed", "User creation failed.");
    }
}
