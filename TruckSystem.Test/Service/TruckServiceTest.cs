using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TruckSystem.CrossCutting.AutoMapper;
using TruckSystem.DAL.Context;
using TruckSystem.DAL.IRepository;
using TruckSystem.DAL.Repository;
using TruckSystem.Domain.Vehicles.Model;
using TruckSystem.Domain.Vehicles.ViewModels;
using TruckSystem.Service.Services;
using TruckSystem.Test.Fixture;
using Xunit;

namespace TruckSystem.Test.Service
{
    [Trait("ServiceTest", "Truck")]
    public class TruckServiceTest : IDisposable
    {
        private Mock<IModelRepository> _modelRepoMock;

        private IMapper _mapperCreated;

        public TruckServiceTest()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapperCreated = configMapper.CreateMapper();
        }

        [Fact(DisplayName = "Cadastrar_Truck_Valido")]
        public async Task Cadastrar_Truck_Valido()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SqlContext>()
                    .UseInMemoryDatabase(databaseName: $"Test_Sql{Guid.NewGuid()}")
                    .Options;

            TruckCollectionFixture._truckFixture = new TruckFixture();

            var listModelObj = TruckCollectionFixture._truckFixture.GetModelList();
            var TruckObj = TruckCollectionFixture._truckFixture.GetTruckViewModelRequestMock();
            //Act
            using (var context = new SqlContext(options))
            {
                _modelRepoMock = new Mock<IModelRepository>();
                _modelRepoMock.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(listModelObj);

                var truckRepo = new TruckRepository(context, new Mock<ILogger<TruckRepository>>().Object);
                
                var service = new TruckService(
                         truckRepo,
                         _mapperCreated,
                         new Mock<ILogger<TruckService>>().Object,
                         _modelRepoMock.Object
                         );

                await service.InsertTruck(TruckObj);

                var retorno = await service.GetAllTrucks();
                
                //Assert
                Assert.NotEmpty(retorno.Result);                
            }
        }

        [Fact(DisplayName = "Atualizar_Truck_Valido")]
        public async Task Atualizar_Truck_Valido()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SqlContext>()
                    .UseInMemoryDatabase(databaseName: $"Test_Sql{Guid.NewGuid()}")
                    .Options;

            TruckCollectionFixture._truckFixture = new TruckFixture();

            var listModelObj = TruckCollectionFixture._truckFixture.GetModelList();
            var TruckObj = TruckCollectionFixture._truckFixture.GetTruckViewModelRequestMock();
            //Act
            using (var context = new SqlContext(options))
            {
                _modelRepoMock = new Mock<IModelRepository>();
                _modelRepoMock.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(listModelObj);

                var truckRepo = new TruckRepository(context, new Mock<ILogger<TruckRepository>>().Object);

                var service = new TruckService(
                         truckRepo,
                         _mapperCreated,
                         new Mock<ILogger<TruckService>>().Object,
                         _modelRepoMock.Object
                         );

                await service.InsertTruck(TruckObj);

                var nomeNovo = Guid.NewGuid().ToString();
                TruckObj.Nome = nomeNovo;
                await service.UpdateTruck(TruckObj, TruckObj.Id);

                var retorno = await service.GetAllTrucks();

                //Assert
                Assert.Equal(retorno.Result.First().Nome, nomeNovo);
            }
        }


        [Fact(DisplayName = "Deleta_Truck_Valido")]
        public async Task Deleta_Truck_Valido()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SqlContext>()
                    .UseInMemoryDatabase(databaseName: $"Test_Sql{Guid.NewGuid()}")
                    .Options;

            TruckCollectionFixture._truckFixture = new TruckFixture();

            var listModelObj = TruckCollectionFixture._truckFixture.GetModelList();
            var TruckObj = TruckCollectionFixture._truckFixture.GetTruckViewModelRequestMock();
            //Act
            using (var context = new SqlContext(options))
            {
                _modelRepoMock = new Mock<IModelRepository>();
                _modelRepoMock.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(listModelObj);

                var truckRepo = new TruckRepository(context, new Mock<ILogger<TruckRepository>>().Object);

                var service = new TruckService(
                         truckRepo,
                         _mapperCreated,
                         new Mock<ILogger<TruckService>>().Object,
                         _modelRepoMock.Object
                         );

                await service.InsertTruck(TruckObj);

                await service.DeleteTruck(TruckObj.Id);

                var retorno = await service.GetAllTrucks();

                //Assert
                Assert.Empty(retorno.Result);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
