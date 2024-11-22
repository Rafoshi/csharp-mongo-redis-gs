using Dapper;
using MySqlConnector;
using web_app_domain;

namespace web_app_repository
{
    public class ConsumoRepository : IConsumoRepository
    {
        private readonly MySqlConnection mySqlConnection;

        public ConsumoRepository() {
            string connectionString = "Server=localhost;Database=sys;User=root;Password=123;";
            mySqlConnection = new MySqlConnection(connectionString);

        }
        public async Task<IEnumerable<Consumo>> ListarConsumos()
        {

            await mySqlConnection.OpenAsync();
            string query = "select id, consumo_energetico, status, tipo_consumo , data_criacao from consumos;";
            var consumos = await mySqlConnection.QueryAsync<Consumo>(query);
            await mySqlConnection.CloseAsync();


            return consumos;

        }
        public async Task SalvarConsumo(Consumo consumo) {
            await mySqlConnection.OpenAsync();
            string sql = "insert into consumos(id, consumo_energetico, status, tipo_consumo , data_criacao) values(@Status, @ConsumoEnergetico, @TipoConsumo, @DataCriacao);";
            await mySqlConnection.ExecuteAsync(sql, consumo);
            await mySqlConnection.CloseAsync();
        }
        public async Task AtualizarConsumo(Consumo consumo) { 
            await mySqlConnection.OpenAsync();
            string sql = "Update consumos set status = @Status, consumo_energetico = @ConsumoEnergetico, tipo_consumo=@TipoConsumo,data_criacao = @DataCriacao where Id=@id";
            await mySqlConnection.ExecuteAsync(sql, consumo);
            await mySqlConnection.CloseAsync();

        }
        public async Task RemoverConsumo(int id)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"delete from consumos where Id=@id";
            await mySqlConnection.ExecuteAsync(sql, new { id });
            await mySqlConnection.CloseAsync();

        }

    }
}
