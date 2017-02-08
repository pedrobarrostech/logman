using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logman_core.entities;
using logman_core.enums;

namespace logman_core.interfaces
{
    public interface ILogService : IBaseService<Log>
    {
        void RecordLog(Log log);
        List<Log> GetByFilter(int code, string description, TypeLogEnum type);
        List<Log> GetLog();
    }
}
