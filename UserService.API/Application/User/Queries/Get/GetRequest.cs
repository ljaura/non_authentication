using MediatR;
using UserService.Domain.Common;

namespace UserService.API.Application.User.Queries.Get
{
    public class GetRequest : IRequest<EntityResponseModel>
    {
        public int UserId { get; set; }
    }
}
