using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.SERVICE_PROCESSOR
{
    public interface IServiceProcessor
    {
        void Process();
        void NotifyError(string message);
    }
}
