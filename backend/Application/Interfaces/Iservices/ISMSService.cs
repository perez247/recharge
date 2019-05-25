using System.Threading.Tasks;
using Application.Entities.UserEntity.Command.GeneratePhoneToken;

namespace Application.Interfaces.Iservices
{
    public interface ISMSService
    {
        Task SendAsync(PhoneTokenGenerated PhoneDetails);
    }
}