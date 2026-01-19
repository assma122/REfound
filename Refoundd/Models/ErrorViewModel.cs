namespace Refoundd.Models
{
    public class ErrorViewModel
    {
        // Gets or sets the request ID for error tracking
        public string? RequestId { get; set; }
        // Indicates whether to show the request ID
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
