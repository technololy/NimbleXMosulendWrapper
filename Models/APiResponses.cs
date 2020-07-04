using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MosulendWrapper.Models
{
    public class APiResponses
    {
        public class APIResponses<T>
        {
            public string ResponseCode { get; set; }
            public string ResponseMessage { get; set; }
            public string Message { get; set; }
            public object Errors { get; set; }
            public T Data { get; set; }
        }


        public class CustomerExistsResponse
        {
            public bool CustomerExists { get; set; }
            public string CustomerType { get; set; }
        }

       public  class Organization
        {
            public int EmployerId { get; set; }
            public int OrganizationId { get; set; }
            public int AgencyId { get; set; }
            public string OrganizationName { get; set; }
            public string EmployerName { get; set; }
            public string AgencyName { get; set; }
            public DateTime LastDateModified { get; set; }
        }
    }
}
