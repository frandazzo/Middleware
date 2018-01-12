using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public class SendToBackHandler : IFailedMessageHandler
    {
        const int MAX_RETRIES = 3;
        MessageQueue deadLetterQueue;

        public SendToBackHandler(MessageQueue deadLetterQueue)
        {
            this.deadLetterQueue = deadLetterQueue;
        }

        public TransactionAction HandleFailedMessage(Message message, MessageQueueTransaction transaction)
        {
            message.Priority = MessagePriority.Lowest;
            message.AppSpecific++;
            if (message.AppSpecific > MAX_RETRIES)
            {
                //Trace.WriteLine("Sending to dead-letter queue");
                deadLetterQueue.Send(message, transaction);
            }
            else
            {
               // Trace.WriteLine("Sending to back of queue");
                message.DestinationQueue.Send(message, transaction);
            }
            return TransactionAction.COMMIT;
        }
    }
}
