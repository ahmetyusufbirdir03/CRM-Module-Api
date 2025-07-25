using System.Text.Json.Serialization;

namespace Applicaton.DTOs;

public class ResponseDto<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    [JsonPropertyName("errors")]
    public List<String>? Errors { get; set; }

    public static ResponseDto<T> Success(int statusCode, T? data)
    {
        return new ResponseDto<T> { Data = data, StatusCode = statusCode};
    }

    public static ResponseDto<T> Success(int statusCode)
    {
        return new ResponseDto<T> { StatusCode = statusCode };
    }

    public static ResponseDto<T> Fail(int statusCode, List<string> errors)
    {
        return new ResponseDto<T> { Errors = errors, StatusCode = statusCode };
    }

    public static ResponseDto<T> Fail(int statusCode, string error)
    {
        return new ResponseDto<T> { Errors = new List<string> { error }, StatusCode = statusCode };
    }

    public static ResponseDto<T> Fail(string error)
    {
        return new ResponseDto<T> { Errors = new List<string> { error }, StatusCode = 400 };
    }

    public static ResponseDto<T> Fail(List<string> errors)
    {
        return new ResponseDto<T> { Errors = errors , StatusCode = 400};
    }


}
