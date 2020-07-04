using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MosulendWrapper.Interfaces
{
    public interface IAPiService
    {
        public  Task<T> Post<T>(object model, string endPoint, string contentType = "application/json");
        public  Task<T> Get<T>(string endPoint, string contentType = "application/json");

        public Task<T> GetWithBaseURL<T>(string endPoint, string contentType = "application/json");
    }
}
