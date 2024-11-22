using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class ConsumoControllerTest
    {
        private readonly Mock<IConsumoRepository> _userRepositoryMock;
        private readonly ConsumoController _controller;

        public ConsumoControllerTest()
        {
            _userRepositoryMock = new Mock<IConsumoRepository>();
            _controller = new ConsumoController(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_ConsumoOk()
        {
            List<Consumo> consumos = new() {
                new Consumo() {

                }
            };
            _userRepositoryMock.Setup(r => r.ListarConsumos()).ReturnsAsync(consumos);
            IActionResult result = await _controller.GetConsumo();
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult? okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(consumos), JsonConvert.SerializeObject(okResult.Value));
        }

        [Fact]
        public async Task Get_ListarRetornarNotFound()
        {
            _userRepositoryMock.Setup(u => u.ListarConsumos()).ReturnsAsync(([]));
            IActionResult result = await _controller.GetConsumo();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_listarConsumosOk()
        {
            List<Consumo> consumos = new List<Consumo>() { new()  {

                }
            };
        }

        [Fact]
        public async Task Post_listarConsumosOk()
        {
            //Falha pois o repositorio não esta separado do controller
            Consumo consumo = new()
            {

            };

            _userRepositoryMock.Setup(r => r.SalvarConsumo(It.IsAny<Consumo>())).Returns(Task.CompletedTask);
            var result = await _controller.Post(consumo);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Put_listarConsumosOk()
        {
            //Falha pois o repositorio não esta separado do controller
            Consumo consumo = new()
            {

            };

            _userRepositoryMock.Setup(r => r.AtualizarConsumo(consumo)).Returns(Task.CompletedTask);
            var result = await _controller.Put(consumo);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_listarConsumosOk()
        {
            //Falha pois o repositorio não esta separado do controller
            Consumo consumo = new()
            {

            };

            _userRepositoryMock.Setup(r => r.SalvarConsumo(It.IsAny<Consumo>())).Returns(Task.CompletedTask);
            var result = await _controller.Delete(1);

            Assert.IsType<OkResult>(result);
        }
    }
}
