using recharge.api.Helpers.CustomDataTypes.EventArgTypes;

namespace recharge.api.Core.Interfaces
{
    public interface ITransactionRepository
    {
         void RecordTransaction(object source, CustomTransactionEventArgs e);
    }
}