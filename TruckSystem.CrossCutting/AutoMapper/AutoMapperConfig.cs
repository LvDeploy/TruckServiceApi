using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using TruckSystem.Domain.Vehicles.Model;
using TruckSystem.Domain.Vehicles.ViewModels;

namespace TruckSystem.CrossCutting.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<TruckViewModel.Request, Truck>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Nome))
                .ForMember(x => x.ManufactureYear, opt => opt.MapFrom(x => x.AnoFabricacao))
                .ForMember(x => x.ModelYear, opt => opt.MapFrom(x => x.AnoModelo))
                .ForMember(x => x.Model, opt => opt.MapFrom(x => new Model { Name = x.Modelo }));
            CreateMap<Truck, TruckViewModel.Response>()
               .ForMember(x => x.Nome, opt => opt.MapFrom(x => x.Name))
               .ForMember(x => x.AnoModelo, opt => opt.MapFrom(x => x.ModelYear))
               .ForMember(x => x.AnoFabricacao, opt => opt.MapFrom(x => x.ManufactureYear))
               .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
               .ForMember(x => x.DataCadastro, opt => opt.MapFrom(x => x.DateCreated))
               .ForMember(x => x.Modelo, opt => opt.MapFrom(x => x.Model.Name));
        }
    }
}
