using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using logman_core.entities;
using logman_core.enums;
using logman_core.interfaces;
using logman_core.repositories;
using Newtonsoft.Json;

namespace logman_core.services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository; 

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void RecordLog(Log log){

            var typesAllowed = new TypeLogEnum [] 
                        { 
                            TypeLogEnum.Error, 
                            TypeLogEnum.Warning, 
                            TypeLogEnum.Information
                        };

            if(typesAllowed.Contains(log.Type)){
                try{
                    
                    log.Date = DateTime.Now;
                    _logRepository.Insert(log);
                }catch(Exception e){

                    Console.WriteLine(e.Message);
                    throw new Exception("An error ocurred, please contact the administrator.");
                }
            }else{
                throw new Exception("Just allowed 3 types of Log: Information, Error, Waring.");
            }
        }

        public List<Log> GetLog(){

            var content = GetContent();
            var logs = (content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)).ToList();
            
            return logs.Select(s => JsonConvert.DeserializeObject<Log>(s)).ToList();
        }

        public List<Log> GetByFilter(int code, string description, TypeLogEnum type)
        {
            var logEntities = GetLog().AsEnumerable();           

            logEntities = code != 0 ? logEntities.Where(w => w.Code == code) : logEntities;
            
            logEntities = !string.IsNullOrEmpty(description) 
                             ?   logEntities.Where(w => description.Contains( w.Description )) 
                             :   logEntities;
            
            logEntities = type != 0 ? logEntities.Where(w => w.Type == type) : logEntities;

            return logEntities.ToList();
        }

         private string GetContent(){
            return _logRepository.Get();
        }
    }
}
