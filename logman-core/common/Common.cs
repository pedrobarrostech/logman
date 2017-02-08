using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using logman_core.entities;
using logman_core.interfaces;
using logman_core.repositories;

namespace logman_core.common
{
    public class Common<T> : interfaces.ICommons<T> where T : class
    {
        public T Deserialize(string json)
        {
            throw new NotImplementedException();
        }

        public string Serialize(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
