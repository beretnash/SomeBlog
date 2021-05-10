namespace SomeBlog.Application.DataTransferObjects.Email
{
    public class EmailRequest
    {
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
