using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using TruckSystem.Domain.Common;

namespace TruckSystem.Domain.Vehicles.ViewModels
{
    public class TruckViewModel
    {
        public class Request
        {
            [JsonIgnore]
            public int Id { get; set; }

            public string Nome { get; set; }

            public int AnoFabricacao { get; set; }

            public int AnoModelo { get; set; }

            public string Modelo { get; set; }
        }

        public class Response 
        {
            public int Id { get; set; }

            public string Nome { get; set; }

            public int AnoFabricacao { get; set; }

            public int AnoModelo { get; set; }

            public string Modelo { get; set; }

            public DateTime DataCadastro { get; set; }
        }
    }
}
