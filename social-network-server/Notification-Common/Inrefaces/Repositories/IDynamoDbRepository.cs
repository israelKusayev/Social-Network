

using System.Collections.Generic;

namespace Notification_Common.Interfaces.Repositories
{
    public interface IDynamoDbRepository<T>
    {
        bool Add(T record);
        T Get<K>(K recordId);
        bool Update(T record);

        List<T> GetAll<K>(K recordId);
    }
}