using System.IO;
using System.Threading.Tasks;
using Application.Entities.UserEntity.Command.GeneratePhoneToken;
using Application.Interfaces.Iservices;

namespace Infrastructure.Implementation
{
    public class SMSService : ISMSService
    {
        TextWriter writer;

        public async Task SendAsync(PhoneTokenGenerated PhoneDetails)
        {
            await WriteToFile(PhoneDetails.PhoneToken.Token);
        }

        public async Task WriteToFile(string data) {
            using (writer = new StreamWriter(Path.GetFullPath("phonetoken.txt"), append: false))
            {
                await writer.WriteLineAsync(data);
            }
        }
    }
}