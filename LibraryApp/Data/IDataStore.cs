using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data
{
    interface IDataStore<T>
    {
        Task<bool> AddDataAsync(T Data);//добавление в базу
        Task<bool> UpdateDataAsync(T Data);//обновление данных в базе
        Task<bool> DeleteDataAsync(T Data);//удаление данных из базы
        Task<T> GetDataAsync(string id);//вытягивание кокретного объекта из базы
        Task<IEnumerable<T>> GetDataAsync(bool forceRefresh = false);//вытягивание всех объектов из базы
    }
}
