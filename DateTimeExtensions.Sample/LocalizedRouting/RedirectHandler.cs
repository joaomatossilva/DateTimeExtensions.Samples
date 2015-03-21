using System.Web;

namespace DateTimeExtensions.Sample.LocalizedRouting
{
    class RedirectHandler : IHttpHandler
    {
        private readonly string _newUrl;

        public RedirectHandler(string newUrl)
        {
            _newUrl = newUrl;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect(_newUrl);
        }
    }
}