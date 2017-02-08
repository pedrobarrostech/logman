using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logman_core.enums;

namespace logman_core.entities
{
    public class Log {

        public Log(int code, string description, TypeLogEnum type)
        {
            this.Code = code;
            this.Description = description;
            this.Type = type;
        }

        public Log()
        {}

        public int Code { get; set; }
        public String Description { get; set; }
        public TypeLogEnum Type { get; set; }
        public DateTime Date { get; set; }

    }
}
