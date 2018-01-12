using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WIN.TECHNICAL.HTTP_HANDLERS
{
    internal class RestHandlerFactory : IHttpHandlerFactory
    {
        // Fields
        internal const string ClientDebugProxyRequestPathInfo = "/jsdebug";
        internal const string ClientProxyRequestPathInfo = "/js";

        // Methods
        public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            //if (IsClientProxyRequest(context.Request.PathInfo))
            //{
            //    return new RestClientProxyHandler();
            //}
            return RestHandler.CreateHandler(context);
        }

        internal static bool IsClientProxyDebugRequest(string pathInfo)
        {
            return string.Equals(pathInfo, "/jsdebug", StringComparison.OrdinalIgnoreCase);
        }

        internal static bool IsClientProxyRequest(string pathInfo)
        {
            if (!string.Equals(pathInfo, "/js", StringComparison.OrdinalIgnoreCase))
            {
                return IsClientProxyDebugRequest(pathInfo);
            }
            return true;
        }

        internal static bool IsRestMethodCall(HttpRequest request)
        {
            if (string.IsNullOrEmpty(request.PathInfo))
            {
                return false;
            }
            if (!request.ContentType.StartsWith("application/json;", StringComparison.OrdinalIgnoreCase))
            {
                return string.Equals(request.ContentType, "application/json", StringComparison.OrdinalIgnoreCase);
            }
            return true;
        }

        internal static bool IsRestRequest(HttpContext context)
        {
            if (!IsRestMethodCall(context.Request))
            {
                return IsClientProxyRequest(context.Request.PathInfo);
            }
            return true;
        }

        public virtual void ReleaseHandler(IHttpHandler handler)
        {
        }
    }


}
