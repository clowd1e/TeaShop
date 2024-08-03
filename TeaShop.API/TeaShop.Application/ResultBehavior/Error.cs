namespace TeaShop.Application.ResultBehavior
{
    public sealed record Error(string Code, string Message, string? PropertyName = null)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static implicit operator Result(Error error) => Result.Fail(error);
    }
}
