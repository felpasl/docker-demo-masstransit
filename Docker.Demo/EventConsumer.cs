using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Demo
{
    public class EventConsumer :
               IConsumer<ValueEntered>
    {
        public async Task Consume(ConsumeContext<ValueEntered> context)
        {
            Console.WriteLine("Value: {0}", context.Message.Value);
        }
    }
}
