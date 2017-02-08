using System.Collections.Generic;
using logman_api.Controllers;
using logman_core.entities;
using logman_core.enums;
using logman_core.interfaces;
using logman_core.services;
using Moq;
using System;
using NUnit.Framework;
using Newtonsoft.Json;

namespace logman.test
{
    [TestFixture]
    public class LogServiceTest
    {

        public void TestServiceGet()
        {
            var logList = GetMockList();
            var service = new Mock<ILogService>();

            service.Setup(s => s.GetLog()).Returns(logList);
            var controller = new LogController(service.Object);

            Assert.That(controller.Get(), Is.EqualTo(logList));
        }

        [TestCase(1, "", 3)]
        public void TestControllerGetByFilter(int code, string description, TypeLogEnum type)
        {
            var logList = GetMockList();
            var service = new Mock<ILogService>();

            service.Setup(s => s.GetLog()).Returns(logList);
            service.Setup(s => s.GetByFilter(code, description, type)).Returns(logList);

            var controller = new LogController(service.Object);

            Assert.That(controller.Get(code, description, type), Is.EqualTo(logList));
        }

        [TestCase(0, "", 1, 2)] // code, description, type, Count Expected
        [TestCase(0, "", 3, 1)] // code, description, type, Count Expected
        public void TestServiceGetByFilter(int code, string description, TypeLogEnum type, int countExpected)
        {
            var logList = GetMockList();
            var logRepository = new Mock<ILogRepository>();

            logRepository.Setup(s => s.Get()).Returns(GetContent());
            var service = new LogService(logRepository.Object);

            Assert.That(service.GetByFilter(code, description, type).Count, Is.EqualTo(countExpected));

        }
        public void TestServiceInsert()
        {
            var logList = GetMockList();
            var log = GetMockEntity();
            var service = new Mock<ILogService>();

            service.Setup(s => s.RecordLog(log)).Verifiable();
            service.VerifyAll();

            var controller = new LogController(service.Object);
            Assert.That(controller.Post(log), Is.EqualTo("Recorded!"));
        }

        private List<Log> GetMockList()
        {
            var logList = new List<Log>(){
                new Log(1,"teste1", TypeLogEnum.Information),
                new Log(12,"teste2", TypeLogEnum.Warning),
                new Log(13,"teste3", TypeLogEnum.Warning),
            };

            return logList;
        }

        private Log GetMockEntity()
        {
            return new Log();
        }

        private string GetContent(){
           string content = ""; 
           GetMockList().ForEach((log) => { content += JsonConvert.SerializeObject(log) + Environment.NewLine; });
           return content;
        }
    }


}