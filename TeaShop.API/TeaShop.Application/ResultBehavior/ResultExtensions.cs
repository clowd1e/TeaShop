namespace TeaShop.Application.ResultBehavior
{
    public static class ResultExtensions
    {
        public static Result<T> ToResult<T>(this T value) 
            => Result<T>.Ok(value);
    }
}
