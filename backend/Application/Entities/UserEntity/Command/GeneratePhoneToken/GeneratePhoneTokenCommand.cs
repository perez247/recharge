using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Infrastructure.Token;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Entities.UserEntity.Command.GeneratePhoneToken
{
    public class GeneratePhoneTokenCommand : TokenCredentials,IRequest<Unit>
    {
        public GeneratePhoneTokenCommand(ClaimsPrincipal user)
            :base(user)
        {
        }
    }

    public class GeneratePhoneTokenHandler : IRequestHandler<GeneratePhoneTokenCommand, Unit>
    {
        private readonly IAuthRepository _auth;
        private readonly IMediator _mediator;

        public GeneratePhoneTokenHandler(IAuthRepository auth, IMediator mediator)
        {
            _auth = auth;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(GeneratePhoneTokenCommand request, CancellationToken cancellationToken)
        {

            var phoneToken = await _auth.GeneratePhoneToken(request.UserId);

            if(phoneToken == null)
                throw new NotFoundException(nameof(User), request.UserId);

            await _mediator.Publish(phoneToken);

            return Unit.Value;
            // return phoneToken;
        }
    }

    public class PhoneToken {
        public string Token { get; set; }
        public User User { get; set; }
    }
}