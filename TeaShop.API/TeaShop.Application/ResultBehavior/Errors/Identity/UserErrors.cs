namespace TeaShop.Application.ResultBehavior.Errors.Identity
{
    public static class UserErrors
    {
        public static Error UserNotFound => new("User.UserNotFound", "User not found.");
        public static Error NewPasswordIsSame => new("User.NewPasswordIsSame", "New password is the same as the old one.");
    }
}
