namespace NamaaTalabat.Api.Errors
{
    public class ServerErrorResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ServerErrorResponse(int statuscode, string? Message = null, string? details = null)
            :base(statuscode , Message)
        {
            Details = details;
        }
    }
}
