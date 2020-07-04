using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MosulendWrapper.Models.APiResponses;

namespace MosulendWrapper.Interfaces
{
    public interface IDataService
    {
        public Task<CustomerExistsResponse> GetCustomersExistenceByPhone(string customerID);
        public Task<Organization> GetOrganizationByOrganizationName(string organizationName);

        public Task<dynamic> GetWeatherFromOpenAPI();

    }
}
