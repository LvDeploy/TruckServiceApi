using FluentValidation;
using FluentValidation.Validators;
using System;
using TruckSystem.Domain.Vehicles.Model;

namespace TruckSystem.Domain.Vehicles.Validators
{
    public class TruckValidator : AbstractValidator<Truck>
    {
        public TruckValidator()
        {
            RuleFor(x => x.Model)
              .NotNull()
              .WithMessage("Informe o nome do modelo");

            RuleFor(x => x.ManufactureYear)
              .NotNull()
              .NotEmpty()
              .NotEqual(0)
              .GreaterThan(0)
              .WithMessage("O ano de fabricação deve ser um número válido maior que 0");

            RuleFor(x => x.ModelYear)
              .NotNull()
              .NotEmpty()
              .NotEqual(0)
              .GreaterThan(0)
              .WithMessage("O ano de modelo deve ser um número válido maior que 0");

            RuleFor(x => x.Name)
              .NotNull()
              .NotEmpty()
              .WithMessage("Informe o nome do veículo");

            RuleFor(x => x.ManufactureYear)
              .Must(x => x == DateTime.Now.Year)
              .When(x => x.ModelYear > 0)
              .WithMessage("Ano de fabricação precisa ser o atual");

            RuleFor(x => x.ModelYear)
              .Must(x => x == DateTime.Now.Year || x == (DateTime.Now.Year + 1))
              .When(x => x.ModelYear > 0)
              .WithMessage("Ano de modelo precisa ser o atual ou o ano subsequente");
        }
    }
}
