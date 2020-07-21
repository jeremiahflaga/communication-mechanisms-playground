using System;
using System.Threading.Tasks;
using MassTransit;

namespace GettingStarted
{
    public class Message
    { 
        public string Text { get; set; }
    }

    public class Program
    {
        public static async Task Main()
        {
            // await UsingInMemory.Execute();
            await UsingRabbitMq.Execute();
            
        }
    }
}
