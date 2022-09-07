using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Common;
using UserService.Infrastructure.Configuration;

namespace UserService.API.Application.User.Queries.Get
{
    public class GetHandler : IRequestHandler<GetRequest, EntityResponseModel>
    {
        private readonly UserContext _context;

        public GetHandler(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<EntityResponseModel> Handle(GetRequest request, CancellationToken cancellationToken)
        {

            var response = await _context.UserAddresses
                .Where(x => x.UserId == request.UserId && !x.IsDeleted)
                .Select(x => new GetResponseModel()
                {
                   Id = x.Id,
                   Street = x.Street
                }).ToListAsync();


            return new EntityResponseModel()
            {
                Data = response
            };
        }
    }
}
