using System.Net;
using System.Net.Http;
using System.Text;

namespace octapush.SPEditor.Utils
{
    public static class ResponseMessageExtension
    {
        public static StringContent ToStringContent(this string message, string mediaType = "application/json")
        {
            return new StringContent(message, Encoding.UTF8, mediaType);
        }

        public static HttpResponseMessage SetStatusCode(this HttpResponseMessage hrm, HttpStatusCode newStatus)
        {
            hrm.StatusCode = newStatus;
            return hrm;
        }

        public static HttpResponseMessage SetMessage(this HttpResponseMessage hrm, string message)
        {
            hrm.Content = message.ToStringContent();
            return hrm;
        }

        public static HttpResponseMessage ToHttpResponseMessage(this string message,
                                                                HttpStatusCode status = HttpStatusCode.OK)
        {
            return new HttpResponseMessage
            {
                StatusCode = status,
                Content = message.ToStringContent()
            };
        }

        public static HttpResponseMessage HttpResponse_BadRequest()
        {
            return "Request could not be understood by the server."
                .ToHttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public static HttpResponseMessage HttpResponse_Ambiguous()
        {
            return "Request information has multiple representation."
                .ToHttpResponseMessage(HttpStatusCode.Ambiguous);
        }

        public static HttpResponseMessage HttpResponse_Conflict()
        {
            return "Request could not be carried out because of a conflict on the server."
                .ToHttpResponseMessage(HttpStatusCode.Conflict);
        }

        public static HttpResponseMessage HttpResponse_NoContent()
        {
            return "Request has been successfully processed and that the response is intentionally blank."
                .ToHttpResponseMessage(HttpStatusCode.NoContent);
        }

        public static HttpResponseMessage HttpResponse_NotFound()
        {
            return "Requested resource does not exist on the server."
                .ToHttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}