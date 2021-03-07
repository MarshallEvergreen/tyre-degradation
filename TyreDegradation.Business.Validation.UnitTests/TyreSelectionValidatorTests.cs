using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TyreDegradation.Contract.Enums;

namespace TyreDegradation.Business.Validation.UnitTests
{
    [TestClass]
    public class TyreSelectionValidatorTests
    {
        private readonly SelectedTyresBuilder _selectedTyresBuilder = new();
        private readonly TyreSelectionValidator _tyreSelectionValidator = new();

        [TestMethod]
        public void Result_true_AllTyresSameCompound()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.SuperSoft, "F1")
                .FrontRightTyre(TyreCompound.SuperSoft, "F1")
                .RearLeftTyre(TyreCompound.SuperSoft, "F1")
                .RearRightTyre(TyreCompound.SuperSoft, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(true);
            result.Message.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Result_false_FrontLeftDifferent()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.Soft, "F1")
                .FrontRightTyre(TyreCompound.SuperSoft, "F1")
                .RearLeftTyre(TyreCompound.SuperSoft, "F1")
                .RearRightTyre(TyreCompound.SuperSoft, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(false);
            result.Message.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Result_false_FrontRightDifferent()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.SuperSoft, "F1")
                .FrontRightTyre(TyreCompound.Soft, "F1")
                .RearLeftTyre(TyreCompound.SuperSoft, "F1")
                .RearRightTyre(TyreCompound.SuperSoft, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(false);
            result.Message.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Result_false_RearLeftDifferent()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.SuperSoft, "F1")
                .FrontRightTyre(TyreCompound.SuperSoft, "F1")
                .RearLeftTyre(TyreCompound.Soft, "F1")
                .RearRightTyre(TyreCompound.SuperSoft, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(false);
            result.Message.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Result_false_RearRightDifferent()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.SuperSoft, "F1")
                .FrontRightTyre(TyreCompound.SuperSoft, "F1")
                .RearLeftTyre(TyreCompound.SuperSoft, "F1")
                .RearRightTyre(TyreCompound.Soft, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(false);
            result.Message.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Result_false_AllDifferent()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.SuperSoft, "F1")
                .FrontRightTyre(TyreCompound.Soft, "F1")
                .RearLeftTyre(TyreCompound.Medium, "F1")
                .RearRightTyre(TyreCompound.Hard, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(false);
            result.Message.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Result_true_FrontAxleFamilyMatch_RearAxleFamilyMatch()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.SuperSoft, "F1")
                .FrontRightTyre(TyreCompound.SuperSoft, "F1")
                .RearLeftTyre(TyreCompound.SuperSoft, "F1")
                .RearRightTyre(TyreCompound.SuperSoft, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(true);
            result.Message.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Result_false_FrontAxleFamilyMismatch_RearAxleFamilyMatch()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.SuperSoft, "F1")
                .FrontRightTyre(TyreCompound.SuperSoft, "F2")
                .RearLeftTyre(TyreCompound.SuperSoft, "F1")
                .RearRightTyre(TyreCompound.SuperSoft, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(false);
            result.Message.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Result_false_FrontAxleFamilyMatch_RearAxleFamilyMismatch()
        {
            var tyres = _selectedTyresBuilder
                .FrontLeftTyre(TyreCompound.SuperSoft, "F1")
                .FrontRightTyre(TyreCompound.SuperSoft, "F1")
                .RearLeftTyre(TyreCompound.SuperSoft, "F2")
                .RearRightTyre(TyreCompound.SuperSoft, "F1")
                .Build();

            var result = _tyreSelectionValidator.Validate(tyres);

            result.Result.Should().Be(false);
            result.Message.Should().NotBeEmpty();
        }
    }
}