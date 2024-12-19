namespace ChefConnect.Application.Common.Response
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ServiceResponse(T data, string message = null)
        {
            IsSuccess = true;
            Data = data;
            Message = message;
        }

        public ServiceResponse(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }
}
