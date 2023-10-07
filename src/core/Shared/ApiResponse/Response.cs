
namespace Shared.ApiResponse
{
    public class Response<T>
    {
        public Response(int statusCode, T data, string message = null)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
            Success = true;
        }

        public Response(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            Message = errorMessage;
            Success = false;
        }
  
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
