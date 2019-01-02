using Identity_Common.models;

namespace Identity_Common.interfaces
{
    public interface IDynamoDbRepository<T>
    {
        bool Add(T record);
        T Get<K>(K recordId);
        bool Update(T record);
    }
}