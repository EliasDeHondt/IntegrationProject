namespace Business_Layer;

public interface IManager<T>
{
    T Add(T item);
    T Update(T item);
    T Remove(T item);
    T Get(int id);
    List<T> GetAll();
}