
using System;
using System.Collections.Generic;
using TruckSystem.Domain.Vehicles.Model;
using TruckSystem.Domain.Vehicles.Validators;
using TruckSystem.Domain.Vehicles.ViewModels;
using Xunit;

namespace TruckSystem.Test.Fixture
{
    [CollectionDefinition(nameof(TruckCollectionFixture))]
    public static class TruckCollectionFixture
    {
        public static TruckFixture _truckFixture { get; set; }
    }

    public sealed class TruckFixture
    {
        public TruckViewModel.Request GetTruckViewModelRequestMock()
        {
            return new TruckViewModel.Request
            {
                Id = 1, 
                AnoFabricacao = 2019,
                AnoModelo = 2020,
                Modelo = "FM",
                Nome = "LeviTruck"
            };
        }

        public TruckViewModel.Response GetTruckViewModelResponseMock()
        {
            return new TruckViewModel.Response
            {
                Id = 1,
                AnoFabricacao = DateTime.Now.Year,
                AnoModelo = DateTime.Now.Year,
                Modelo = "FM",
                Nome = "LeviTruck",
                DataCadastro = DateTime.Now
            };
        }

        public Truck GetTruckMock()
        {
            return new Truck
            {
                Id = 1,
                ModelYear = DateTime.Now.Year,
                ManufactureYear = DateTime.Now.Year,
                Model = new Model { Id = 4, Name = "FH" },
                Name = "LeviTruck",
                DateCreated = DateTime.Now
            };
        }

        public List<Model> GetModelList()
        {
            return new List<Model>
            {
               new Model {
                Id = 1,
                Name = "OP"
               },
               new Model {
                Id = 2,
                Name = "PR"
               },
               new Model {
                Id = 3,
                Name = "FF"
               },
               new Model {
                Id = 5,
                Name = "FM"
               },
               new Model {
                Id = 4,
                Name = "FH"
               }
            };
        }

        public List<Truck> GetListTruckMock()
        {
            return new List<Truck>
            {
                new Truck {
                Id = 1,
                ModelYear = DateTime.Now.Year,
                ManufactureYear = DateTime.Now.Year,
                Model = new Model { Id = 4, Name = "FH" },
                Name = "LeviTruck",
                DateCreated = DateTime.Now
               },
               new Truck {
                Id = 2,
                ModelYear = 2019,
                ManufactureYear = 2020,
                Model = new Model { Id = 5, Name = "FM" },
                Name = "LeviTruck2",
                DateCreated = DateTime.Now
               }
            };
        }
    }
}
