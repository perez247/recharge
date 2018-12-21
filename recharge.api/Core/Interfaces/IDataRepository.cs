using System.Threading.Tasks;
using recharge.api.Helpers.CustomDataTypes.EventArgTypes;
using recharge.api.Core.Models;

namespace recharge.api.Core.Interfaces
{
    public interface IDataRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        void Update<T>(T entity) where T:class;
        Task<bool> SaveAll();
    }
}