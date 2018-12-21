using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Core.Interfaces;
using recharge.api.Core.Models;
using recharge.api.Helpers;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;

namespace recharge.api.Persistence.Repository
{
    public class PointRepository : IPointRepository
    {
        private readonly Decimal bonusPercentage = 0.05m;
        private readonly DataContext _context;
        private readonly IDataRepository _repo;

        public PointRepository(DataContext context, IDataRepository repo)
        {
            _context = context;
            _repo = repo;
        }
        public async void CreatePoint(object source, UserEventArgs e)
        {
            var point = new Point() 
                        {
                            User = e.User,
                            Points = 0
                        };
            _repo.Add(point);

            if(!await _repo.SaveAll()){
                _repo.Delete(e.User);
                await _repo.SaveAll();
            }

        }

        public async Task<Point> GetUserPoint(string userId) {
            // You can work on the decryption here;
            return await _context.Points.FirstOrDefaultAsync(p => p.UserId.ToString() == userId);
        }

        public void AddPoint(Decimal amount, User user) {
            var Userbonus =  Functions.GetBonus(amount);
            AddToUser(Functions.GetBonus(amount), user);
            AddToReferer(Functions.GetBonus(amount, false), user);
        }

        public void AddToUser(Decimal amount, User user) {
            user.Point.Points += amount;
            user.Expires = DateTime.Now.AddDays(61);
        }

        public void AddToReferer(Decimal amount, User user) {
            if(user.Referer == null || !user.Referer.PhoneNumberConfirmed || user.Referer.Expires < DateTime.Now)
                return;

            user.Referer.Point.Points += amount;
        }
    }
}