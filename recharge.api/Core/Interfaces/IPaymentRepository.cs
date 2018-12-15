using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Models;

namespace recharge.api.Core.Interfaces
{
    public interface IPaymentRepository
    {
        void ValidatePayementDetail(MobileRechargeRequestResourse mobileRechargeRequestResource, User user);
        User ProcessDatabasePayment(MobileRechargeRequestResourse mobileRechargeRequestResource, User user);
   }
}