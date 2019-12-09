using TruckSystem.Test.Fixture;
using Xunit;
using FluentValidation.TestHelper;
using TruckSystem.Domain.Vehicles.Validators;
using System;

namespace TruckSystem.Test.Validators
{
    [Trait("ValidatorsTest", "Truck")]
    public class TruckValidatorTest : IDisposable
    {
        public TruckValidator _validator = new TruckValidator();

        [Fact(DisplayName = "Test_TruckValidatorObject_Valido")]
        public void Test_TruckValidatorObject_Valido()
        {
            //arrange
            TruckCollectionFixture._truckFixture = new TruckFixture();

            var obj = TruckCollectionFixture._truckFixture.GetTruckMock();
            
            //act, assert
            _validator.ShouldNotHaveValidationErrorFor(x => x.Model, obj);
            _validator.ShouldNotHaveValidationErrorFor(x => x.ModelYear, obj);
            _validator.ShouldNotHaveValidationErrorFor(x => x.ManufactureYear, obj);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, obj);
        }


        [Fact(DisplayName = "Test_TruckValidatorObject_Invalido")]
        public void Test_TruckValidatorObject_Invalido()
        {
            //arrange
            TruckCollectionFixture._truckFixture = new TruckFixture();

            var obj = TruckCollectionFixture._truckFixture.GetTruckMock();
            obj.Model = null;
            obj.Name = null;
            obj.ModelYear = 2999;
            obj.ManufactureYear = 3999;
            //act, assert
            _validator.ShouldHaveValidationErrorFor(x => x.Model, obj);
            _validator.ShouldHaveValidationErrorFor(x => x.ModelYear, obj);
            _validator.ShouldHaveValidationErrorFor(x => x.ManufactureYear, obj);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, obj);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
