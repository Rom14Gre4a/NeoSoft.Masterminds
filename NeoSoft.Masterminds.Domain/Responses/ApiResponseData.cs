using System.Net;

namespace NeoSoft.Masterminds.Domain.Models.Responses
{
    public class ApiResponse<T> : ApiResponseBase
    {
        public T Data { get; set; }

        public static implicit operator ApiResponse<T>(T value)
        {
            return new ApiResponse<T>
            {
                Data = value,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }
    }
}
