namespace FamiliesAPI.Services.Common
{
    public class ServicesResult<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        public static ServicesResult<T> SuccessfulOperation(T result)
        {
            return new ServicesResult<T>
            {
                Success = true,
                Result = result
            };
        }

        public static ServicesResult<T> FailedOperation(int statusCode, string message, Exception exception = null/*, List<T> result = null*/)
        {
            return new ServicesResult<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Exception = exception?.ToString()
            };
        }
    }
}
