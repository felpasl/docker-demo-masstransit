using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Docker.Demo
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Docker World!");

            var serviceProvider = ContainerConfiguration.Configure();

            var bc = serviceProvider.GetService<IBusControl>();
            bc.Start();

            serviceProvider.GetService<ContinuousRunningProcessor>().Process();
        }
    }
}
