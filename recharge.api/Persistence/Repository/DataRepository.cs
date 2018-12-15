using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recharge.api.Persistence.Repository.EventArgTypes;
using recharge.api.Core.Interfaces;
using recharge.api.Core.Models;

namespace recharge.api.Persistence.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;
        public DataRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Point> GetUserPoint(string userId) {
            // You can work on the decryption here;
            return await _context.Points.FirstOrDefaultAsync(p => p.UserId.ToString() == userId);
        }

        public async void CreatePoint(object source, UserEventArgs e)
        {
            var point = new Point() 
                        {
                            User = e.User
                        };
            _context.Add(point);

            if(!await SaveAll()){
                Delete(e.User);
                await SaveAll();
            }

        }

        public async void AddReferer(object source, UserEventArgs e)
        {
            // var referer = _context.Users.Find
            var point = new Point() 
                        {
                            User = e.User
                        };
            _context.Add(point);

            if(!await SaveAll()){
                Delete(e.User);
                await SaveAll();
            }

        }
    }
}