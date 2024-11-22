using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MySqlConnector;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;
using web_app_domain;
using web_app_repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_app_performance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoController : ControllerBase
    {
        private static ConnectionMultiplexer redis;
        private readonly IConsumoRepository _repository;

        public ConsumoController(IConsumoRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetConsumo()
        {
            string key = "getconsumo";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyExpireAsync(key, TimeSpan.FromSeconds(10));
            string user =await db.StringGetAsync(key);
            if (!string.IsNullOrEmpty(user)) {
                return Ok(user);
            
            }
            var consumos = await _repository.ListarConsumos();
            string consumosJson = JsonConvert.SerializeObject(consumos);
            await db.StringSetAsync(key,consumosJson);

            return Ok(consumos);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Consumo consumo)
        {
            await _repository.SalvarConsumo(consumo);

            string key = "getconsumo";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Consumo consumo)
        {
            await _repository.AtualizarConsumo(consumo);

            string key = "getconsumo";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.RemoverConsumo(id);

            string key = "getconsumo";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }
    }
}
