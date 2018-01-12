using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WIN.TECHNICAL.HTTP_HANDLERS
{
    internal class WebServiceParameterData
    {
        // Fields
        private int _index;
        private ParameterInfo _param;
        private string _paramName;
        private Type _paramType;

        // Methods
        internal WebServiceParameterData(ParameterInfo param, int index)
        {
            this._param = param;
            this._index = index;
        }

        internal WebServiceParameterData(string paramName, Type paramType, int index)
        {
            this._paramName = paramName;
            this._paramType = paramType;
            this._index = index;
        }

        // Properties
        internal int Index
        {
            get
            {
                return this._index;
            }
        }

        internal ParameterInfo ParameterInfo
        {
            get
            {
                return this._param;
            }
        }

        internal string ParameterName
        {
            get
            {
                if (this._param != null)
                {
                    return this._param.Name;
                }
                return this._paramName;
            }
        }

        internal Type ParameterType
        {
            get
            {
                if (this._param != null)
                {
                    return this._param.ParameterType;
                }
                return this._paramType;
            }
        }
    }


}
