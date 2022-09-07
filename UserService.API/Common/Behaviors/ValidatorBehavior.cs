using FluentValidation;
using MediatR;
using UserService.API.Infrastructure.Validator;
using System.Text.Json;

namespace UserService.API.Common.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest> _validator;

        public ValidatorBehavior(IValidator<TRequest> validator, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation(
               "[{Prefix}] Handle request={X-RequestData} and response={X-ResponseData}",
               nameof(ValidatorBehavior<TRequest, TResponse>), typeof(TRequest).Name, typeof(TResponse).Name);

            _logger.LogDebug($"Handling {typeof(TRequest).FullName} with content {JsonSerializer.Serialize(request)}");

            await _validator.HandleValidation(request);

            _logger.LogInformation($"Handled {typeof(TRequest).FullName}");

            return await next();
        }
    }
}