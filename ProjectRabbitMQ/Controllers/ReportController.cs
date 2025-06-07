using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProjectRabbitMQ.Bus;
using ProjectRabbitMQ.Relatorio;
using ProjectRabbitMQ.Report;

namespace ProjectRabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Lista.Relatorios);
        }

       
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

      
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromQuery] string name, IPublishBus bus, CancellationToken ct = default)
        {
            var x = "teste";
            _ = Task.Run(async () =>
            {
                int soma = 1;
                while (soma <= 100)
                {
                    var solicitacao = new SolicitacaoRelatorio
                    {
                        Id = Guid.NewGuid(),
                        Nome = name + soma,
                        Status = "Pendente",
                        HorarioProcessamento = null
                    };

                    Lista.Relatorios.Add(solicitacao);

                    var eventResquest = new RelatorioSolicitadoEvent(solicitacao.Id, solicitacao.Nome);

                    await bus.PublishAsync(eventResquest, ct);

                    soma++;
                }
            }, ct);

            return Ok();
        }

      
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

       
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
