using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalCalcWpf.Model
{
    public interface ICommand
    {
        bool Add();
        bool Remove();
        bool Update();
        ObservableCollection<T> GetDataList<T>();
        T GetData<T>();
    }
}
