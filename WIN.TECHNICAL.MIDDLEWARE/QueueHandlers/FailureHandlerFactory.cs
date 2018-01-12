using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public class FailureHandlerFactory
    {
        public static IFailedMessageHandler CreateFailureHandler(FailureHandlerType type, string queueName, string deadQueueName, string retryQueueName)
        {
            IFailedMessageHandler failureHandler = null;
            if (type == FailureHandlerType.Discard )
            {
                failureHandler = new DiscardMessageHandler();
            }
            if (type == FailureHandlerType.AlwaysRollback )
            {
                failureHandler = new AlwaysRollBackHandler();
            }
            else if (type == FailureHandlerType.SendToBack )
            {
                failureHandler = new SendToBackHandler(
                        CreateAndGetQueue(deadQueueName));
            }
            else if (type == FailureHandlerType.RetrySendToDead )
            {
                failureHandler = new RetrySendToDeadLetterQueueHandler(
                        CreateAndGetQueue(deadQueueName ));
            }
            else if (type == FailureHandlerType.SeparateRetry )
            {
                failureHandler = new SeparateRetryQueueHandler(
                        CreateAndGetQueue(retryQueueName ));
            }


            return failureHandler;
        }

        protected static System.Messaging.MessageQueue CreateAndGetQueue(string queueName)
        {
            if (!MessageQueue.Exists(queueName))
            {
                MessageQueue.Create(queueName, true);
            }
            return new MessageQueue(queueName);
        }
    }

    public enum FailureHandlerType
    {
        AlwaysRollback,
        Discard,
        RetrySendToDead,
        SendToBack,
        SeparateRetry
    }
}
