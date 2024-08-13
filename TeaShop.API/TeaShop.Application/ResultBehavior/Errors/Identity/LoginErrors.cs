namespace TeaShop.Application.ResultBehavior.Errors.Identity
{
    public static class LoginErrors
    {
        public static readonly Error EmailNotFound = new("Login.EmailNotFound", "Email address not found.");
        public static readonly Error InvalidPassword = new("Login.InvalidPassword", "Invalid password.");
    }
}
