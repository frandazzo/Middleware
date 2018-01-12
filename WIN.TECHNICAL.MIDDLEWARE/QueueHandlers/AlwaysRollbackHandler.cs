using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public class AlwaysRollBackHandler : IFailedMessageHandler
    {
        public TransactionAction HandleFailedMessage(Message message, MessageQueueTransaction transaction)
        {
            return TransactionAction.ROLLBACK;
        }
    }
}
