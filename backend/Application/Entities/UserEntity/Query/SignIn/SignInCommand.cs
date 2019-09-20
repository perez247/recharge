using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Infrastructure.Token;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Entities.UserEntity.Query.SignIn
{
    public class SignInCommand : IRequest<SignInModel>
    {
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public string Pin { get; set; }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInModel>
    {
        private readonly IAuthRepository _auth;

        public SignInCommandHandler(IAuthRepository auth)
        {
            _auth = auth;
        }
        public async Task<SignInModel> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user  = await _auth.SignIn($"{request.CountryCode}-{request.PhoneNumber}", request.Pin);

            if (user == null)
                throw new CustomMessageException("Invalid login credentials");

            var response = SignInModel.Create(user);

            // Add token for authorization
            response.Token = TokenFunctions.generateUserToken(user);

            return response;
        }
    }
}