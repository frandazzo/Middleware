using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public class RetrySendToDeadLetterQueueHandler : IFailedMessageHandler
    {
        static string lastMessageId = null;
        static int retries = 0;
        const int MAX_RETRIES = 3;
        MessageQueue deadLetterQueue;

        public RetrySendToDeadLetterQueueHandler(MessageQueue deadLetterQueue)
        {
            this.deadLetterQueue = deadLetterQueue;
        }

        public TransactionAction HandleFailedMessage(Message message, MessageQueueTransaction transaction)
        {
            if (message.Id != lastMessageId)
            {
                retries = 0;
                lastMessageId = message.Id;
            }
            retries++;
            if (retries > MAX_RETRIES)
            {
                //Trace.WriteLine("Sending to dead-letter queue");
                deadLetterQueue.Send(message, transaction);
                return TransactionAction.COMMIT;
            }
            else
            {
                //Trace.WriteLine("Returning message to queue for retry: " + retries);
                return TransactionAction.ROLLBACK;
            }
        }
    }
}
