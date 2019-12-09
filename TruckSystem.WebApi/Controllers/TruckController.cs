using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TruckSystem.Domain.Common;
using TruckSystem.Domain.Vehicles.IService;
using TruckSystem.Domain.Vehicles.Model;
using TruckSystem.Domain.Vehicles.ViewModels;

namespace TruckSystem.WebApi.Controllers
{
    /// <summary>
    /// Controller para manipulação de Caminhão
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TruckController : BaseController
    {
        public readonly ITruckService _truckService;

        /// <summary>
        /// Construtor da controller
        /// </summary>
        /// <param name="TruckViewModel.Response"></param>
        public TruckController(ITruckService truckService)
        {
            _truckService = truckService;
        }

        /// <summary>
        /// Obtem todos os caminhões
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 200)]
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 400)]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTrucks()
        {
            var data = await _truckService.GetAllTrucks();
            return ApplicationResult(data, data.StatusCode);
        }

        /// <summary>
        /// Insere um Caminhão
        /// </summary>
        /// <param name="TruckViewModel.Request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 200)]
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 400)]
        [HttpPost("insert")]
        public async Task<IActionResult> InsertTruck([FromBody] TruckViewModel.Request value)
        {
            var data = await _truckService.InsertTruck(value);
            return ApplicationResult(data, data.StatusCode);
        }

        /// <summary>
        /// Atualiza um caminhão
        /// </summary>
        /// <param name="TruckViewModel.Request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 200)]
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 400)]
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 404)]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTruck([FromRoute] int id, [FromBody] TruckViewModel.Request value)
        {
            var data = await _truckService.UpdateTruck(value, id);
            return ApplicationResult(data, data.StatusCode);
        }

        /// <summary>
        /// Deleta o caminhão
        /// </summary>
        /// <param name="TruckViewModel.Response"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 200)]
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 400)]
        [ProducesResponseType(typeof(BaseResult<TruckViewModel.Response>), 404)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTruck([FromRoute] int id)
        {
            var data = await _truckService.DeleteTruck(id);
            return ApplicationResult(data, data.StatusCode);
        }
    }
}
