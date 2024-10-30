
namespace NamaaTalabat.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statusCode , string? message = null )
        {
            StatusCode = statusCode ;
            Message = message ?? GetDefaltErrorResponse(statusCode);
        }

        private string? GetDefaltErrorResponse(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request " , 
                401 => "Unauthorized Access" , 
                404 => " Not Found" , 
                500=> "Server Error has happened  ",
                _ => null 
            };

        }
    }
}
