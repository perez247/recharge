using System.Threading.Tasks;
using recharge.api.Data.EventArgTypes;
using recharge.api.models;

namespace recharge.api.Data.Interfaces
{
    public interface IDataRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        void Update<T>(T entity) where T:class;
        void BeginTransaction();
        void Commit();
        void RollBack();
        Task<bool> SaveAll();
        Task<Point> GetUserPoint(string userId);
        void CreatePoint(object source, UserEventArgs e);
        void AddReferer(object source, UserEventArgs e);
    }
}