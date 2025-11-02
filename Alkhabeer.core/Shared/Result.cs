namespace Alkhabeer.Core.Shared
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }

        protected Result(bool success, string? message = null)
        {
            IsSuccess = success;
            ErrorMessage = message;
        }

        public static Result Success() => new(true);
        public static Result Failure(string message) => new(false, message);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool success, T? value, string? message = null)
            : base(success, message)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, value);
        public static new Result<T> Failure(string message) => new(false, default, message);
    }
}
