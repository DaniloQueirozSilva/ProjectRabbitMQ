using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ProjectRabbitMQ.Bus;
using System;

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
                    cfg.Host(new Uri("amqp://localhost:5672"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    // Fila principal, nome explícito da fila
                    cfg.ReceiveEndpoint("relatoriosolicitacadoevent", e =>
                    {
                        e.ConfigureConsumer<RelatorioSolicitacadoEventConsumer>(ctx);

                        // Retry imediato in-memory (exemplo 3 tentativas)
                        e.UseMessageRetry(r => r.Immediate(3));

                        // Não precisa configurar manualmente DLQ, MassTransit já cria fila de error automaticamente:
                        // Nome da fila de error será "relatoriosolicitacadoevent_error"


                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
}
