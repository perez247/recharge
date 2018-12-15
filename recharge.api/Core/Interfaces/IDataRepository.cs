using System.Threading.Tasks;
using recharge.api.Persistence.Repository.EventArgTypes;
using recharge.api.Core.Models;

namespace recharge.api.Core.Interfaces
{
    public interface IDataRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        void Update<T>(T entity) where T:class;
        Task<bool> SaveAll();
        Task<Point> GetUserPoint(string userId);
        void CreatePoint(object source, UserEventArgs e);
        void AddReferer(object source, UserEventArgs e);
    }
}