using System.Net;

namespace Bitmischief.Meridios.SmugMug.Entities.Request
{
    public class BaseSmugMugRequest
    {
        public SmugMugRequestResponse Response { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }
}
