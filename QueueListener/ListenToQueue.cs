using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace QueueListener
{
    public class ListenToQueue
    {
        static void Main(string[] args)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

           // var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            var client = QueueClient.CreateFromConnectionString(connectionString, "TaskQueue");

            client.Receive();

            while (true)
            {
                var message = client.Receive();

                if (message != null)
                {
                    try
                    {
                        var test = message.GetBody<string>();
                        var messageId = message.MessageId;
                        var keyValue1 = message.Properties["Task"];
                       // var taskMessageKey = message.Properties["TaskMessageKey"];

                        Console.WriteLine(messageId);
                        Console.WriteLine(keyValue1);
                        //Console.WriteLine(taskMessageKey);

                    }
                    catch (Exception ex)
                    {
                        message.Abandon();
                    }
                }
            }
            
        }
    }
}
