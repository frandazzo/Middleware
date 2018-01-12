using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public interface IFailedMessageHandler
    {
        TransactionAction HandleFailedMessage(Message message, MessageQueueTransaction transaction);
    }

    public enum TransactionAction { ROLLBACK, COMMIT };
}
