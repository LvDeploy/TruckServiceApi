using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckSystem.DAL.IRepository;
using TruckSystem.Domain.Common;
using TruckSystem.Domain.Vehicles.IService;
using TruckSystem.Domain.Vehicles.Model;
using TruckSystem.Domain.Vehicles.ViewModels;

namespace TruckSystem.Service.Services
{
    public class TruckService : ITruckService
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepo;
        private readonly ITruckRepository _truckRepo;
        private readonly ILogger _logger;

        public TruckService( ITruckRepository truckRepo, IMapper mapper, ILogger<TruckService> logger, IModelRepository modelRepo)
        {
            _mapper = mapper;
            _truckRepo = truckRepo;
            _logger = logger;
            _modelRepo = modelRepo;
        }

        public async Task<BaseResult<TruckViewModel.Response>> DeleteTruck(int id)
        {
            try
            {
                var truck = await _truckRepo.GetByIdAsync(id);

                if (truck != null)
                    await _truckRepo.DeleteAsync(truck);
                else
                    return new BaseResult<TruckViewModel.Response> { StatusCode = System.Net.HttpStatusCode.NotFound };

                return new BaseResult<TruckViewModel.Response>();
            }
            catch (Exception ex)
            {
                return new BaseResult<TruckViewModel.Response> { StatusCode = System.Net.HttpStatusCode.BadRequest, Error = ex.Message };
            }
        }

        public async Task<BaseResult<List<TruckViewModel.Response>>> GetAllTrucks()
        {
            try
            {
                var retorno = await _truckRepo.GetAllAsync();
                return new BaseResult<List<TruckViewModel.Response>> { Result = _mapper.Map<List<Truck>, List<TruckViewModel.Response>>(retorno) };        
            }
            catch (Exception ex)
            {
                return new BaseResult<List<TruckViewModel.Response>> { StatusCode = System.Net.HttpStatusCode.BadRequest, Error = ex.Message };
            }
        }

        public async Task<BaseResult<TruckViewModel.Response>> InsertTruck(TruckViewModel.Request truck)
        {
            try
            {
                if (truck.Modelo.ToUpper() != "FH" && truck.Modelo.ToUpper() != "FM")
                    throw new ArgumentException("Apenas os Valores FH e FM são aceitos no modelo de caminhão");

                var modelo = (await _modelRepo.GetAllAsync()).Single(x => x.Name == truck.Modelo.ToUpper());

                var dbObject = _mapper.Map<TruckViewModel.Request, Truck>(truck);

                dbObject.Model = modelo;
                await _truckRepo.SaveAsync(dbObject);

                return new BaseResult<TruckViewModel.Response> { StatusCode = System.Net.HttpStatusCode.OK, Result = _mapper.Map<Truck, TruckViewModel.Response>(dbObject) };
            }
            catch (Exception ex)
            {
                return new BaseResult<TruckViewModel.Response> { StatusCode = System.Net.HttpStatusCode.BadRequest, Error = ex.Message };
            }

        }

        public async Task<BaseResult<TruckViewModel.Response>> UpdateTruck(TruckViewModel.Request truck, int id)
        {
            try
            {
                if (truck.Modelo.ToUpper() != "FH" && truck.Modelo.ToUpper() != "FM")
                    throw new ArgumentException("Apenas os Valores FH e FM são aceitos no modelo de caminhão");

                var modelo = (await _modelRepo.GetAllAsync()).Single(x => x.Name == truck.Modelo.ToUpper());

                var dbObject = await _truckRepo.GetByIdAsync(id);

                if (dbObject == null)
                    return new BaseResult<TruckViewModel.Response> { StatusCode = System.Net.HttpStatusCode.NotFound, Result = _mapper.Map<Truck, TruckViewModel.Response>(dbObject) };

                dbObject.ManufactureYear = truck.AnoFabricacao;
                dbObject.ModelYear = truck.AnoModelo;
                dbObject.Name = truck.Nome;
                dbObject.Model = modelo;
                await _truckRepo.UpdateAsync(dbObject);

                return new BaseResult<TruckViewModel.Response> { StatusCode = System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new BaseResult<TruckViewModel.Response> { StatusCode = System.Net.HttpStatusCode.BadRequest, Error = ex.Message };
            }
        }

        public Task<BaseResult<Truck>> UpdateTruck(Truck truck)
        {
            throw new NotImplementedException();
        }
    }
}
