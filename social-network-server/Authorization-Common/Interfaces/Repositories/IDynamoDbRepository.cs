namespace Authorization_Common.Interfaces.Repositories
{
    public interface IDynamoDbRepository<T>
    {
        T Add(T item);
        T Get<K>(K id);
        T Update(T item);
    }
}