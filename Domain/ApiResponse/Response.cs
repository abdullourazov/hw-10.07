using System.Net;

namespace Domain.ApiResponse;

public class Response<T>
{
      public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    public Response(T? data, string? message = null)
    {
        IsSuccess = true;
        Message = message;
        Data = data;
        StatusCode = (int)HttpStatusCode.OK;
    }

    public Response(HttpStatusCode statusCode, string message)
    {
        IsSuccess = false;
        Message = message;
        StatusCode = (int)statusCode;
        Data = default;
    }

    public static Response<T> Success(T? data = default, string? message = null)
    {
        return new Response<T>(data, message);
    }
    
    public static Response<T> Error(HttpStatusCode statusCode, string message)
    {
        return new Response<T>(statusCode, message);
    }
}
