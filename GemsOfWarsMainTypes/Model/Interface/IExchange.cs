namespace GemsOfWarsMainTypes.Model
{
    public interface IExchange<T> where T : IExchange<T>
    {
        void ReadFromItem(T item);
        void WriteToItem(T item);
    }
}
