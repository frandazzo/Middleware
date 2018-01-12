using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;

namespace WIN.TECHNICAL.MIDDLEWARE.QueueHandlers
{
    

    public class QueueHandler
    {
        private string _queueName = @".\private$\test_queue";
        private string _queueRetryName = @".\private$\test_queue_retry";
        private string _queueDeadName = @".\private$\test_queue_dead_letter";
        private QueueLogger _logger;

        public string QueueName
        {
            get { return _queueName; }
            set { _queueName = value; }
        }


        public string DeadQueueName
        {
            get { return _queueDeadName; }
            set { _queueDeadName = value; }
        }

        public string RetryQueueName
        {
            get { return _queueRetryName; }
            set { _queueRetryName = value; }
        }


        public void SendMessage(string label, object body)
        {
            if (_logger == null)
            {
                _logger = new QueueLogger("SendQueueMessages");
            }

            try
            {
                _logger.AddMessage(String.Format("Send message on {0} with label {1}", _queueName, label));
                CreateAndGetQueue(_queueName).Send(body, label, MessageQueueTransactionType.Single);
                //Trace.WriteLine("Sent message: " + label);
            }
            catch (Exception ex)
            {

                _logger.AddMessage(String.Format("Error sending message on {0} with label {1}: {2}", _queueName, label, ex.Message ));
            }
        }


        public void SendMessage(string label, object body, string queueName, string errorLogPath)
        {
            if (_logger == null)
            {
                _logger = new QueueLogger("SendQueueMessages", errorLogPath );
            }
            try
            {
                _logger.AddMessage(String.Format("Send message on {0} with label {1}", queueName, label));
                CreateAndGetQueue(queueName).Send(body, label, MessageQueueTransactionType.Single);
                //Trace.WriteLine("Sent message: " + label);
            }
            catch (Exception ex)
            {

                _logger.AddMessage(String.Format("Error sending message on {0} with label {1}: {2}", queueName, label, ex.Message));
            }

          
        }


        public  MessageQueue CreateAndGetQueue(string queueName)
        {
            if (!MessageQueue.Exists(queueName))
            {
                MessageQueue.Create(queueName, true);
            }
            return new MessageQueue(queueName);
        }



        

    }
}
