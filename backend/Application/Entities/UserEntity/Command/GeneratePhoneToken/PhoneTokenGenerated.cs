using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Iservices;
using MediatR;
using static Application.Entities.UserEntity.Command.GeneratePhoneToken.GeneratePhoneTokenHandler;

namespace Application.Entities.UserEntity.Command.GeneratePhoneToken
{
    public class PhoneTokenGenerated : INotification
    {
        public PhoneToken PhoneToken { get; set; }

        public class PhoneTokenGeneratedHandler : INotificationHandler<PhoneTokenGenerated>
        {
            private readonly ISMSService _smsService;

            public PhoneTokenGeneratedHandler(ISMSService smsService)
            {
                _smsService = smsService;
            }
            public async Task Handle(PhoneTokenGenerated notification, CancellationToken cancellationToken)
            {
                await _smsService.SendAsync(notification);
            }
        }
    }
}