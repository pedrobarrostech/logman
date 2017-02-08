using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logman_core.entities;
using logman_core.enums;
using logman_core.interfaces;
using logman_core.services;
using Microsoft.AspNetCore.Mvc;

namespace logman_api.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            this._logService = logService;
        }

        // GET api/log
        [HttpGet]
        public List<Log> Get()
        {
            return _logService.GetLog();
        }

        // GET api/log/filter?code=5&description=test&type=2
        [HttpGet("filter")]
        public List<Log> Get(int code, string description, TypeLogEnum type)
        {
            Console.WriteLine($"code:{code} description:{description} type:{type} ");
            return _logService.GetByFilter(code, description, type);
        }

        // POST api/log
        [HttpPost]
        public string Post([FromBody] Log log)
        {  
            var resultado = "";

            try{    

                _logService.RecordLog(log);
                resultado = "Recorded!";
            }catch(Exception e){

                resultado = e.Message;
            }

            return resultado;
        }

        // PUT api/log/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/log/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
