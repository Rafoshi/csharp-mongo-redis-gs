using Dapper;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MySqlConnector;
using StackExchange.Redis;
using web_app_domain;

namespace web_app_repository
{
    public class ConsumoRepository : IConsumoRepository
    {
        public async Task<IEnumerable<Consumo>> ListarConsumos()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var _database = client.GetDatabase("consumboDB");
            var retorno = _database.GetCollection<Consumo>("consumos").Find(_ => true).ToListAsync();
            return await retorno;
        }
        public async Task SalvarConsumo(Consumo consumo) {
            var client = new MongoClient("mongodb://localhost:27017");
            var _database = client.GetDatabase("consumboDB");
            await _database.GetCollection<Consumo>("consumos").InsertOneAsync(consumo);
        }
        public async Task AtualizarConsumo(Consumo consumo) { 
            var client = new MongoClient("mongodb://localhost:27017");
            var _database = client.GetDatabase("consumboDB");
            await _database.GetCollection<Consumo>("consumos").ReplaceOneAsync(x => x.Id == consumo.Id, consumo);
        }
        public async Task RemoverConsumo(string id)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var _database = client.GetDatabase("consumboDB");
            await _database.GetCollection<Consumo>("consumos").DeleteOneAsync(x => x.Id == id);
        }

    }
}
