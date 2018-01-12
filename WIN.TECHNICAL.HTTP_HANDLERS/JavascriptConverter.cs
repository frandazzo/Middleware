using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Permissions;

namespace WIN.TECHNICAL.HTTP_HANDLERS
{
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public abstract class JavaScriptConverter
    {
        // Methods
        protected JavaScriptConverter()
        {
        }

        public abstract object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer);
        public abstract IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer);

        // Properties
        public abstract IEnumerable<Type> SupportedTypes { get; }
    }

 

}
