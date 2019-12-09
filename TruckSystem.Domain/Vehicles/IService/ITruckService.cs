using System.Collections.Generic;
using System.Threading.Tasks;
using TruckSystem.Domain.Common;
using TruckSystem.Domain.Vehicles.Model;
using TruckSystem.Domain.Vehicles.ViewModels;

namespace TruckSystem.Domain.Vehicles.IService
{
    public interface ITruckService
    {
        Task<BaseResult<TruckViewModel.Response>> InsertTruck(TruckViewModel.Request truck);
        Task<BaseResult<TruckViewModel.Response>> DeleteTruck(int id);
        Task<BaseResult<Truck>> UpdateTruck(Truck truck);
        Task<BaseResult<TruckViewModel.Response>> UpdateTruck(TruckViewModel.Request truck, int id);
        Task<BaseResult<List<TruckViewModel.Response>>> GetAllTrucks();
    }
}
