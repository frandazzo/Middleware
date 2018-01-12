using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using System.Diagnostics;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    public abstract class MessageReceiver
    {
        protected MessageQueue incomingQueue;
        protected Message gotMessage;
        protected QueueLogger _logger;
        protected bool _loggerEnabled = false;

        public Message IncomingMessage
        {
            get { return gotMessage; }
        }

        //public MessageReceiver(MessageQueue incomingQueue)
        //{
        //    _loggerEnabled = false;
        //    this.incomingQueue = incomingQueue;
        //    incomingQueue.MessageReadPropertyFilter.SetAll();
        //}

        public MessageReceiver(MessageQueue incomingQueue, bool enableLogger)
        {
            _loggerEnabled = enableLogger;
            this.incomingQueue = incomingQueue;
            incomingQueue.MessageReadPropertyFilter.SetAll();
        }

        protected void Log(string message)
        {
            if (_loggerEnabled)
            {
                if (_logger == null)
                    _logger = new QueueLogger("noesis-queue-recevier");
                _logger.AddMessage(message);
            }
                
        }

        public void receiveMessage(IFailedMessageHandler failedMessageHandler)
        {
            if (incomingQueue == null)
                throw new NullReferenceException("La coda di ricezione non è stata specificata");
            gotMessage = null;


            SetFormatter();


            MessageQueueTransaction transaction = new MessageQueueTransaction();
            transaction.Begin();
           // Message message = null;
            try
            {
                gotMessage = incomingQueue.Receive(TimeSpan.Zero, transaction);
              
                Log("Received message ID: " + gotMessage.Id);
                ProcessMessage();
                transaction.Commit();
                Log("Message processed OK");
            }
            catch (Exception e)
            {
                Log("Message failed");
                TransactionAction transactionAction = TransactionAction.ROLLBACK;

                if (gotMessage == null)
                {
                    Log("Message couldn't be received: " + e.Message);
                    Log("Message couldn't be received (stak: " + e.StackTrace);
                }
                else
                {
                    try
                    {
                        transactionAction = failedMessageHandler.HandleFailedMessage(gotMessage, transaction);
                    }
                    catch (Exception failureHandlerException)
                    {
                        Log("Error during failure handling: " + failureHandlerException.Message);
                    }
                }

                if (transactionAction == TransactionAction.ROLLBACK)
                {
                    transaction.Abort();
                    Log("Transaction rolled back");
                }
                else
                {
                    transaction.Commit();
                    Log("Transaction commited - message removed from queue");
                }
            }
        }

        protected abstract void SetFormatter();
        

        protected abstract void ProcessMessage();
        
    }
}
