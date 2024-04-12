namespace SalCalc.Models
{
    //Создание интерфейса для реализации патерна Комманда
    public interface ICommand
    {
        bool Add();
        bool Remove();
        bool Update();
        List<T> GetDataList<T>();
        T GetData<T>();
    }
}
