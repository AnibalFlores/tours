namespace Viajes365RestApi.Wrappers
{
    public class Response<T>
    {
        public T Element { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }

        public Response() { }

        public Response(T data)
        {
            ErrorCode = 0; 
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Element = data;
        }

    }
}
