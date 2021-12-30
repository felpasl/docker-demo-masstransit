using MassTransit;
using System;

namespace Docker.Demo
{
    internal class ConsolePrinter : IConsolePrinter
    {
        private readonly IPrintSettingsProvider printSettingsProvider;
        readonly IPublishEndpoint publishEndpoint;

        public ConsolePrinter(IPrintSettingsProvider printSettingsProvider, IPublishEndpoint publishEndpoint)
        {
            this.printSettingsProvider = printSettingsProvider;
            this.publishEndpoint = publishEndpoint;
        }

        public void Print(int count)
        {
            if (printSettingsProvider.CanPrint())
            {
                Console.WriteLine($"Current Count {count}");

                publishEndpoint.Publish<ValueEntered>(new
                {
                    Value = count
                }).Wait();
            }
        }
    }
}
