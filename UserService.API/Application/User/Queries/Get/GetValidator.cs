using FluentValidation;
using Microsoft.Extensions.Localization;
using UserService.Infrastructure.Configuration;

namespace UserService.API.Application.User.Queries.Get
{
    public class GetValidator : AbstractValidator<GetRequest>
    {
        private readonly UserContext _context;

        public GetValidator(ILogger<GetRequest> logger, UserContext context,IStringLocalizer<GetLocalize> localizer)
        {
            _context = context;

            RuleFor(command => command.UserId)
              .NotEmpty().WithMessage(localizer["UserIdMust"]);
        }
    }
}
