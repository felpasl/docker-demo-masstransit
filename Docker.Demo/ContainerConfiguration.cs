using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MassTransit;
using System;

namespace Docker.Demo
{
    internal static class ContainerConfiguration
    {
        public static IServiceProvider Configure()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(l => l.AddConsole())
                .Configure<LoggerFilterOptions>(c => c.MinLevel = LogLevel.Debug);
            serviceCollection.AddMassTransit(x =>
            {
                x.AddConsumer<EventConsumer>();

                x.UsingRabbitMq((context, cfg) => {
                    cfg.Host("rabbitmq");
                    cfg.ReceiveEndpoint("event-listener", e =>
                    {
                        e.ConfigureConsumer<EventConsumer>(context);
                    });
                });
            });

            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(serviceCollection);

            containerBuilder.RegisterType<PrintSettingsProvider>().As<IPrintSettingsProvider>().SingleInstance();
            containerBuilder.RegisterType<ConsolePrinter>().As<IConsolePrinter>().SingleInstance();
            containerBuilder.RegisterType<ContinuousRunningProcessor>().SingleInstance();

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
