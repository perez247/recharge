using System;
using System.Threading.Tasks;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Models;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;

namespace recharge.api.Core.Interfaces
{
    public interface IPointRepository
    {
        void CreatePoint(object source, UserEventArgs e);
        void AddPoint(Decimal amount, User user);
        void AddToUser(Decimal amount, User user);
        void AddToReferer(Decimal amount, User user);
        Task<Point> GetUserPoint(string userId);
    }
}