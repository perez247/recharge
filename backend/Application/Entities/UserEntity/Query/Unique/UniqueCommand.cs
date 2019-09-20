using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Entities.UserEntity.Query.Unique
{
    public class UniqueCommand : IRequest<bool>
    {
        public string Value { get; set; }
    }

    public class UniqueCommandHanler : IRequestHandler<UniqueCommand, bool>
    {
        private readonly IAuthRepository _auth;

        public UniqueCommandHanler(IAuthRepository auth)
        {
            _auth = auth;
        }
        public async Task<bool> Handle(UniqueCommand request, CancellationToken cancellationToken)
        {
            return await _auth.UniquePhoneNumber(request.Value);
        }
    }
}