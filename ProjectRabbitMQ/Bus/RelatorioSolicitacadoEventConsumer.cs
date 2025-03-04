using MassTransit;
using ProjectRabbitMQ.Relatorio;
using ProjectRabbitMQ.Report;
using System.Runtime.InteropServices;

namespace ProjectRabbitMQ.Bus
{
    public class  RelatorioSolicitacadoEventConsumer : IConsumer<RelatorioSolicitadoEvent>
    {
        private readonly ILogger<RelatorioSolicitacadoEventConsumer> _logger;

        public RelatorioSolicitacadoEventConsumer(ILogger<RelatorioSolicitacadoEventConsumer> looger)
        {
            _logger = looger;
        }
        public async Task Consume(ConsumeContext<RelatorioSolicitadoEvent> context)
        {

            _logger.LogInformation($"Processando Relatório : {context.Message.Id} - {context.Message.Name}");


            var relatorio = Lista.Relatorios.FirstOrDefault(x => x.Id == context.Message.Id);

            relatorio.HorarioProcessamento = DateTime.Now;
            relatorio.Status = "Processado";

            await Task.Delay(10000);

        }
    }

}
