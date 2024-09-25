namespace DistributionCenter.Infraestructure.Tests.Validators;

using Infraestructure.Validators.Components.Builders.Interfaces;
using Infraestructure.Validators.Extensions;

public class ValidationExtensionsTestsNumbers
{
    [Fact]
    public void WhenNotNull_ShouldReturnValidBuilder_WhenDoubleIsNotNull()
    {
        Mock<IValidationBuilder<double?>> builder = new();
        _ = builder.Setup(x => x.When(It.IsAny<Func<double?, bool>>())).Returns(builder.Object);

        IValidationBuilder<double?> result = ValidationExtensions.WhenNotNull(builder.Object);

        // Assert
        Assert.NotNull(result);
        builder.Verify(x => x.When(It.IsAny<Func<double?, bool>>()), Times.Once);
    }

    [Fact]
    public void WhenNotNull_ShouldThrowArgumentNullException_WhenBuilderIsNull()
    {
        IValidationBuilder<double?> builder = null!;

        _ = Assert.Throws<ArgumentNullException>(() => ValidationExtensions.WhenNotNull(builder));
    }

    [Fact]
    public void WhenNotEmpty_ShouldNotPassValidation_WhenValueIsZero()
    {
        Mock<IValidationBuilder<double>> builder = new();
        Func<double, bool> validationFunc = null!;

        _ = builder.Setup(x => x.When(It.IsAny<Func<double, bool>>()))
            .Callback<Func<double, bool>>(func => validationFunc = func)
            .Returns(builder.Object);

        IValidationBuilder<double> result = ValidationExtensions.WhenNotEmpty(builder.Object);

        // Assert
        Assert.NotNull(result);

        Assert.NotNull(validationFunc);

        bool isValid = validationFunc(0);
        Assert.False(isValid);

        builder.Verify(x => x.When(It.IsAny<Func<double, bool>>()), Times.Once);
    }

    [Fact]
    public void WhenNotEmpty_ShouldReturnValidBuilder_WhenDoubleIsNotZero()
    {
        Mock<IValidationBuilder<double>> builder = new();
        _ = builder.Setup(x => x.When(It.IsAny<Func<double, bool>>())).Returns(builder.Object);

        IValidationBuilder<double> result = ValidationExtensions.WhenNotEmpty(builder.Object);

        // Assert
        Assert.NotNull(result);
        builder.Verify(x => x.When(It.IsAny<Func<double, bool>>()), Times.Once);
    }

    [Fact]
    public void WhenNotEmpty_ShouldThrowArgumentNullException_WhenBuilderIsNull()
    {
        IValidationBuilder<double> builder = null!;

        _ = Assert.Throws<ArgumentNullException>(() => ValidationExtensions.WhenNotEmpty(builder));
    }

    [Fact]
    public void WhenNotEmptyNullable_ShouldReturnValidBuilder_WhenNullableDoubleIsNotZero()
    {
        Mock<IValidationBuilder<double?>> builder = new();
        _ = builder.Setup(x => x.When(It.IsAny<Func<double?, bool>>())).Returns(builder.Object);

        IValidationBuilder<double?> result = ValidationExtensions.WhenNotEmpty(builder.Object);

        // Assert
        Assert.NotNull(result);
        builder.Verify(x => x.When(It.IsAny<Func<double?, bool>>()), Times.Once);
    }

    [Fact]
    public void WhenNotEmptyNullable_ShouldThrowArgumentNullException_WhenBuilderIsNull()
    {
        IValidationBuilder<double?> builder = null!;

        _ = Assert.Throws<ArgumentNullException>(() => ValidationExtensions.WhenNotEmpty(builder));
    }

}
