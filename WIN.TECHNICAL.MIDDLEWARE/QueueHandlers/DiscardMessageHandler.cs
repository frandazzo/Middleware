using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public class DiscardMessageHandler : IFailedMessageHandler
    {
        public TransactionAction HandleFailedMessage(Message message, MessageQueueTransaction transaction)
        {
            //Trace.WriteLine("Message discarded");
            return TransactionAction.COMMIT;
        }
    }
}
