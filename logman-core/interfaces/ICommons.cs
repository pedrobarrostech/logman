using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logman_core.interfaces
{
    public interface ICommons<T> where T : class
    {
       string Serialize(T obj);

       T Deserialize(string json);
    }
}
