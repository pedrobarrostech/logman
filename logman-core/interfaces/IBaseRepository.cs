using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logman_core.interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Insert(T obj);

        string Get();
    }
}
