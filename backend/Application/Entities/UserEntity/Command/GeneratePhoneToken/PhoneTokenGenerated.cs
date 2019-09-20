using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Iservices;
using Domain.Entities;
using MediatR;

namespace Application.Entities.UserEntity.Command.GeneratePhoneToken
{
    public class PhoneTokenGenerated : INotification
    {
        public string Token { get; set; }
        public User User { get; set; }
    }

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