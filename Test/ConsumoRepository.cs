using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class ConsumoRepository
    {
        private readonly Mock<IConsumoRepository> _userRepositoryMock;
        private readonly ConsumoController _controller;

        public ConsumoRepository()
        {
            _userRepositoryMock = new Mock<IConsumoRepository>();
            _controller = new ConsumoController(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Delete_ConsumoOk()
        {
            var consumo = new Consumo()
            {

            };

            _userRepositoryMock.Setup(r => r.SalvarConsumo(consumo)).Returns(Task.CompletedTask);
            _userRepositoryMock.Setup(r => r.RemoverConsumo(consumo.Id)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(consumo.Id);
            Assert.IsType<OkResult>(result);
            var okResult = result as OkResult;
        }

        [Fact]
        public async Task Get_ConsumoOk()
        {
            var consumos = new List<Consumo>() {
                new() {
               

                }, new() {
               
                }
            };

            _userRepositoryMock.Setup(r => r.ListarConsumos()).ReturnsAsync(consumos);
            var result = await _controller.GetConsumo();
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(consumos), JsonConvert.SerializeObject(okResult.Value));
        }
    }
}
