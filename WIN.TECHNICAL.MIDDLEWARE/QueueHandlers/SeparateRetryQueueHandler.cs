using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public class SeparateRetryQueueHandler : IFailedMessageHandler
    {
        MessageQueue retryQueue;

        public SeparateRetryQueueHandler(MessageQueue retryQueue)
        {
            this.retryQueue = retryQueue;
        }

        public TransactionAction HandleFailedMessage(Message message, MessageQueueTransaction transaction)
        {
            //Trace.WriteLine("Sending to retry queue");
            retryQueue.Send(message, transaction);
            return TransactionAction.COMMIT;
        }
    }
}
