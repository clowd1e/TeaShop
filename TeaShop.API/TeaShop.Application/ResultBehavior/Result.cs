using FluentValidation.Results;

namespace TeaShop.Application.ResultBehavior
{
    public class Result
    {
        protected Result(bool isSuccess, string message, IEnumerable<Error> errors)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; }
        public IEnumerable<Error> Errors { get; }

        public static Result Fail(string message)
            => new(false, message, []);

        public static Result Fail(ValidationResult validationResult)
            => new(false,
                string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage)),
                validationResult.Errors.Select(x => new Error("Validation", x.ErrorMessage, x.PropertyName)));

        public static Result Fail(Error error)
            => new(false, error.Message, [error]);

        public static Result Ok() 
            => new(true, string.Empty, []);
    }

    public class Result<T> : Result
    {
        private Result(bool isSuccess, string message, IEnumerable<Error> errors, T value)
            : base(isSuccess, message, errors)
        {
            Value = value;
        }

        public T Value { get; }

        public new static Result<T> Fail(string message)
            => new(false, message, [], default!);

        public new static Result<T> Fail(Error error)
            => new(false, error.Message, [error], default!);

        public static Result<T> Ok(T value)
            => new(true, string.Empty, [], value);

        public static implicit operator Result<T>(Error error) => Fail(error);
    }
}
