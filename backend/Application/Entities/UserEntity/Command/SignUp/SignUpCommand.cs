using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Entities.UserEntity.Command.GeneratePhoneToken;
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
        public string Pin { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ReferersCountryCode { get; set; }
        public string ReferersPhoneNumber { get; set; }

        public string Phone { get; set; }
        public string ReferersPhone { get; set; }
    }

    public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpDto>
    {
        private readonly IAuthRepository _auth;
        
        private readonly IMediator _mediator;

        public SignUpHandler(IAuthRepository auth, IMediator mediator)
        {
            _auth = auth;
            _mediator = mediator;
        }
        public async Task<SignUpDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {   
            var phoneNumber = (PhoneNumber)$"${request.CountryCode}-${request.PhoneNumber}";
            string referersPhoneNumber;

            // Try getting referers phone number
            try {
                var ReferersPhoneNumber = (PhoneNumber)$"${request.ReferersCountryCode}-${request.ReferersPhoneNumber}";
                referersPhoneNumber = ReferersPhoneNumber.ToString();
            }
            catch(Exception) {
                referersPhoneNumber = null;
            }

            var newUser = new User() {
                UserName = $"{request.CountryCode}-{request.PhoneNumber}",
                PhoneNumber = $"{request.CountryCode}-{request.PhoneNumber}"
            };

            var result = await _auth.SignUp(newUser, request.Pin, referersPhoneNumber);

            if (result.User == null)
                throw new CreationFailureException(nameof(User), result.Errors);

            var response = SignUpDto.Create(result.User);
            response.PhoneToken = result.ConfirmPhoneNumberToken;

            // Add token for authorization
            response.Token = TokenFunctions.generateUserToken(newUser);
            
            await _mediator.Publish(new PhoneTokenGenerated() { Token = result.ConfirmPhoneNumberToken, User = result.User });

            return response;

        }
    }

    public class SignUpResult 
    {
        public string Errors { get; set; }
        public User User { get; set; }
        public string ConfirmPhoneNumberToken { get; set; }
    }
}