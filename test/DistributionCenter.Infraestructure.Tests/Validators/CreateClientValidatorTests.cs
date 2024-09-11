// namespace DistributionCenter.Infraestructure.Tests.Validators;
//
// using DistributionCenter.Commons.Errors.Interfaces;
// using DistributionCenter.Commons.Results;
// using DistributionCenter.Infraestructure.DTOs.Concretes.Clients;
// using DistributionCenter.Infraestructure.Validators.Core;
//
// public class CreateClientValidatorTests
// {
//     private readonly CreateClientValidator _validator;
//
//     public CreateClientValidatorTests()
//     {
//         _validator = new CreateClientValidator();
//     }
//
//     [Fact]
//     public void ShouldReturnErrorWhenNameIsEmpty()
//     {
//         CreateClientDto dto =
//             new()
//             {
//                 Name = "",
//                 LastName = "",
//                 Email = "",
//             };
//
//         Result result = _validator.Validate(dto);
//
//         foreach (IError error in result.Errors)
//         {
//             Console.WriteLine(error.Description);
//         }
//     }
// }
