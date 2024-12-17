using ChefConnect.Domain.Common;

namespace ChefConnect.API.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(ServiceResponse<T> serviceResponse)
        {
            Success = serviceResponse.IsSuccess;
            Message = serviceResponse.Message;
            Data = serviceResponse.Data;
        }
    }
}
