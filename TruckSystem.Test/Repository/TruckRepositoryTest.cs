using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using TruckSystem.DAL.Context;
using TruckSystem.DAL.Repository;
using TruckSystem.Test.Fixture;
using Moq;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace TruckSystem.Test.Repository
{
    [Trait("RepositoryTest", "Truck")]
    public class TruckRepositoryTest : IDisposable
    {
        public TruckRepositoryTest()
        {
            ;
        }

        [Fact(DisplayName = "Ao_Final_Da_Execucao_Deve_Gerar_Um_Registro_Valido")]
        public async Task Ao_Final_Da_Execucao_Deve_Gerar_Um_Registro_Valido()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase(databaseName: $"Test_Sql{Guid.NewGuid()}")
                .Options;

            TruckCollectionFixture._truckFixture = new TruckFixture();

            var obj = TruckCollectionFixture._truckFixture.GetTruckMock();

            //Act
            using (var context = new SqlContext(options))
            {
                var repo = new TruckRepository(context, new Mock<ILogger<TruckRepository>>().Object);
                await repo.SaveAsync(obj);
                var retorno = await repo.GetAllAsync();

                //Assert
                Assert.NotNull(retorno);
                Assert.NotEmpty(retorno);
                Assert.Equal(obj.Id, retorno.First().Id);
            }
        }

        [Fact(DisplayName = "Ao_Final_Da_Execucao_Deve_Atualizar_Um_Registro_Valido")]
        public async Task Ao_Final_Da_Execucao_Deve_Atualizar_Um_Registro_Valido()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase(databaseName: $"Test_Sql{Guid.NewGuid()}")
                .Options;

            TruckCollectionFixture._truckFixture = new TruckFixture();

            var obj = TruckCollectionFixture._truckFixture.GetTruckMock();

            //Act
            using (var context = new SqlContext(options))
            {
                var repo = new TruckRepository(context, new Mock<ILogger<TruckRepository>>().Object);
                await repo.SaveAsync(obj);
                
                obj.Name = "UpdateAnotherTruck123";
                await repo.UpdateAsync(obj);
                var retorno = await repo.GetByIdAsync(obj.Id);

                //Assert
                Assert.Equal(retorno.Name, obj.Name);
                Assert.Equal(obj.Id, retorno.Id);
            }
        }

        [Fact(DisplayName = "Ao_Final_Da_Execucao_Deve_Deletar_Um_Registro_Valido")]
        public async Task Ao_Final_Da_Execucao_Deve_Deletar_Um_Registro_Valido()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase(databaseName: $"Test_Sql{Guid.NewGuid()}")
                .Options;

            TruckCollectionFixture._truckFixture = new TruckFixture();

            var obj = TruckCollectionFixture._truckFixture.GetTruckMock();

            //Act
            using (var context = new SqlContext(options))
            {
                var repo = new TruckRepository(context, new Mock<ILogger<TruckRepository>>().Object);
                await repo.SaveAsync(obj);

                await repo.DeleteAsync(obj);
                var retorno = await repo.GetByIdAsync(obj.Id);

                //Assert
                Assert.Null(retorno);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
