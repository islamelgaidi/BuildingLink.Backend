namespace Driver.API.Domain.Results
{
    public record Result<T>
    {
        public T Data { get; private set; }
        public bool Success { get;private set; }
        public string Description { get; private set; }

        private Result(bool success,T data, string description)
        {
            Success = success;
            Data = data;
            Description = description;
        }
        public static Result<T> SuccessResult(T data) => new Result<T>(true, data, string.Empty);
        public static Result<T> FailureResult(string description) => new Result<T>(false,default(T), description);


    }
}
