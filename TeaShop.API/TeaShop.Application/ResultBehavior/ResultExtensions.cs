using FluentValidation.Results;

namespace TeaShop.Application.ResultBehavior
{
    public static class ResultExtensions
    {
        public static Result ToResult(this ValidationResult validationResult)
            => Result.Fail(validationResult);
        public static Result<T> ToResult<T>(this ValidationResult validationResult)
            => Result<T>.Fail(validationResult);
        public static Result<T> ToResult<T>(this T value) 
            => Result<T>.Ok(value);
    }
}
