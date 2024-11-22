using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace web_app_domain
{
    public class Consumo
    {
        [BsonId]
        public string Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public double ConsumoEnergetico { get; set; }
        public string TipoConsumo { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}
