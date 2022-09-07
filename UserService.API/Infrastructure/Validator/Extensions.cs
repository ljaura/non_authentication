using FluentValidation;
using FluentValidation.Results;

namespace UserService.API.Infrastructure.Validator
{
    public static class Extensions
    {
        private static ValidationResultModel ToValidationResultModel(this ValidationResult validationResult)
        {
            return new ValidationResultModel(validationResult);
        }
        public static async Task HandleValidation<TRequest>(this IValidator<TRequest> validator, TRequest request)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToValidationResultModel());
            }
        }
    }
}
