using web_app_domain;

namespace web_app_repository
{
    public  interface IConsumoRepository 
    {
        Task<IEnumerable<Consumo>> ListarConsumos();
        Task SalvarConsumo(Consumo consumo);
        Task AtualizarConsumo(Consumo consumo);
        Task RemoverConsumo(string id);
    }
}
