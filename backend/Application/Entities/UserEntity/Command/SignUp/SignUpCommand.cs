using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Infrastructure.Token;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Entities.UserEntity.Command.SignUp
{
    public class SignUpCommand : IRequest<SignUpDto>
    {
        public string Password { get; set; }
        public string Username { get; set; }

        public string Phone { get; set; }
    }

    public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpDto>
    {
        private readonly IAuthRepository _auth;

        public SignUpHandler(IAuthRepository auth)
        {
            _auth = auth;
        }
        public async Task<SignUpDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User() {
                UserName = request.Username,
                PhoneNumber = request.Phone
            };

            var result = await _auth.SignUp(newUser,request.Password);

            if (result.User == null)
                throw new CreationFailureException(nameof(User), result.Errors);

            var response = SignUpDto.Create(newUser);

            // Add token for authorization
            response.Token = TokenFunctions.generateUserToken(newUser);

            return response;

        }
    }

    public class SignUpResult 
    {
        public string Errors { get; set; }
        public User User { get; set; }
    }
}