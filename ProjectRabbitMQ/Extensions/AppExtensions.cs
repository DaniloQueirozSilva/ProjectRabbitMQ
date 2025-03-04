using MassTransit;
using ProjectRabbitMQ.Bus;

namespace ProjectRabbitMQ.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services) 
        {

            services.AddTransient<IPublishBus, PublishBus>();
            services.AddMassTransit(busConfigurator =>
            {

                busConfigurator.AddConsumer<RelatorioSolicitacadoEventConsumer>();
                busConfigurator.UsingRabbitMq((ctx, cfg) => 
                { 

                    cfg.Host(new Uri("amqp://localhost:5672"), host =>
                    {
                        host.Username("admin");
                        host.Password("admin");
                    });

                    cfg.ConfigureEndpoints(ctx);

                });
            });
        }
    }
}
