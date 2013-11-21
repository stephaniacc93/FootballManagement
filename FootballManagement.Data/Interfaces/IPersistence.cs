using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagement.Data.Interfaces
{
    interface IPersistence <T>
    {
        T Create(T Entity);
        T Read(int ID);
        T Update(T Entity);
        bool Delete(T Entity);
        List<T> GetList();
    }
}
