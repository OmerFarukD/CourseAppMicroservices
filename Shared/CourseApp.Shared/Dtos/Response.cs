using System.Text.Json.Serialization;

namespace CourseApp.Shared.Dtos;

public sealed class Response<T>
{
    public T Data { get;  init; }
    [JsonIgnore]
    public int StatusCode { get; init; }

    public bool IsSuccessful { get; init; }

    public List<string> Errors { get; set; } 

    public static Response<T> Success(T data,int statusCode)=> new Response<T>{Data =data,StatusCode = statusCode, IsSuccessful = true};
    public static Response<T> Success(int statusCode)=> new Response<T>{Data = default(T), StatusCode = statusCode, IsSuccessful = true};

    public static Response<T> Fail(List<string> errors, int statusCode) => new Response<T>
        { Errors = errors, StatusCode = statusCode, IsSuccessful = false };

    public static Response<T> Fail(string error, int statusCode) => new Response<T>
    {
        Errors = new List<string>{error}, StatusCode = statusCode,IsSuccessful = false
    };
    
}