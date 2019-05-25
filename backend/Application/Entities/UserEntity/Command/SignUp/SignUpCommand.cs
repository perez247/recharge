using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using MediatR;

namespace Application.Entities.UserEntity.Command.SignUp
{
    public class SignUpCommand : IRequest<SignUpDto>
    {
        public string Password { get; set; }
        public string Username { get; set; }

        public string Phone { get; set; }
        public string CountryCode { get; set; }
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
                Email = "null@gmail.com",
                PhoneNumber = request.Phone
            };

            var result = await _auth.SignUp(newUser,request.Password);

            if (result.User == null)
                throw new CreationFailureException(nameof(User), result.Errors);

            return SignUpDto.Create(newUser);

        }
    }

    public class SignUpResult 
    {
        public string Errors { get; set; }
        public User User { get; set; }
    }
}