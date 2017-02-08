using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using logman_core.entities;
using logman_core.interfaces;
using Newtonsoft.Json;

namespace logman_core.repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public string Get()
        {
            var path = GetPathFromLog();
            if(!File.Exists(path))
                throw new Exception("No Log Specified for Today.");

            return  File.ReadAllText(path);
        }

        private string GetDataPath(){
            return $"{Directory.GetCurrentDirectory()}\\logman-core\\data\\";
        }

        private string GetPathFromLog(){
            return Path.Combine(GetDataPath(), $"{DateTime.Now:ddMMyyyy}.txt");
        }

        public void Insert(T obj)
        {
            var logPath = GetPathFromLog();
            var objSerialized = JsonConvert.SerializeObject(obj);
            
            if(File.Exists(logPath)){
                
               File.AppendAllText(logPath, $"{ objSerialized } { Environment.NewLine }" );
            }else{

                var logFile = System.IO.File.OpenWrite(logPath);
                using(var logWriter = new System.IO.StreamWriter(logFile)){
                 
                    logWriter.WriteLine($"{ objSerialized } { Environment.NewLine }");
                }
            }
        }
    }
}
