using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.Services.Protocols;

namespace WIN.TECHNICAL.HTTP_HANDLERS
{
    internal class CustomScriptHandlerFactory : IHttpHandlerFactory
    {
        // Fields
        private IHttpHandlerFactory _restHandlerFactory = new RestHandlerFactory();
        private IHttpHandlerFactory _webServiceHandlerFactory = new WebServiceHandlerFactory();

        // Methods
        public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            IHttpHandlerFactory factory;
            if (RestHandlerFactory.IsRestRequest(context))
            {
                factory = this._restHandlerFactory;
            }
            else
            {
                factory = this._webServiceHandlerFactory;
            }
            IHttpHandler originalHandler = factory.GetHandler(context, requestType, url, pathTranslated);
            bool flag = originalHandler is IRequiresSessionState;
            if (originalHandler is IHttpAsyncHandler)
            {
                if (flag)
                {
                    return new AsyncHandlerWrapperWithSession(originalHandler, factory);
                }
                return new AsyncHandlerWrapper(originalHandler, factory);
            }
            if (flag)
            {
                return new HandlerWrapperWithSession(originalHandler, factory);
            }
            return new HandlerWrapper(originalHandler, factory);
        }

        public virtual void ReleaseHandler(IHttpHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            ((HandlerWrapper)handler).ReleaseHandler();
        }

        // Nested Types
        private class AsyncHandlerWrapper : CustomScriptHandlerFactory.HandlerWrapper, IHttpAsyncHandler, IHttpHandler
        {
            // Methods
            internal AsyncHandlerWrapper(IHttpHandler originalHandler, IHttpHandlerFactory originalFactory)
                : base(originalHandler, originalFactory)
            {
            }

            public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
            {
                return ((IHttpAsyncHandler)base._originalHandler).BeginProcessRequest(context, cb, extraData);
            }

            public void EndProcessRequest(IAsyncResult result)
            {
                ((IHttpAsyncHandler)base._originalHandler).EndProcessRequest(result);
            }
        }

        private class AsyncHandlerWrapperWithSession : CustomScriptHandlerFactory.AsyncHandlerWrapper, IRequiresSessionState
        {
            // Methods
            internal AsyncHandlerWrapperWithSession(IHttpHandler originalHandler, IHttpHandlerFactory originalFactory)
                : base(originalHandler, originalFactory)
            {
            }
        }

        internal class HandlerWrapper : IHttpHandler
        {
            // Fields
            private IHttpHandlerFactory _originalFactory;
            protected IHttpHandler _originalHandler;

            // Methods
            internal HandlerWrapper(IHttpHandler originalHandler, IHttpHandlerFactory originalFactory)
            {
                this._originalFactory = originalFactory;
                this._originalHandler = originalHandler;
            }

            public void ProcessRequest(HttpContext context)
            {
                this._originalHandler.ProcessRequest(context);
            }

            internal void ReleaseHandler()
            {
                this._originalFactory.ReleaseHandler(this._originalHandler);
            }

            // Properties
            public bool IsReusable
            {
                get
                {
                    return this._originalHandler.IsReusable;
                }
            }
        }

        internal class HandlerWrapperWithSession : CustomScriptHandlerFactory.HandlerWrapper, IRequiresSessionState
        {
            // Methods
            internal HandlerWrapperWithSession(IHttpHandler originalHandler, IHttpHandlerFactory originalFactory)
                : base(originalHandler, originalFactory)
            {
            }
        }
    }


}
