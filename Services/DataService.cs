using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MosulendWrapper.Models.APiResponses;

namespace MosulendWrapper.Services
{
    public class DataService: Interfaces.IDataService
    {
        private readonly Interfaces.IAPiService aPiService;

        public DataService(Interfaces.IAPiService aPiService)
        {
            this.aPiService = aPiService;
        }

        /// <summary>
        /// GetCustomersExistenceByPhone to get if customer exits on mosulend. returns a model with true or false
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<CustomerExistsResponse> GetCustomersExistenceByPhone(string phoneNumber)
        {
            var endpoint = "customer/exist/";
            var model = new { phoneNumber = phoneNumber };
            var response =await aPiService.Post<CustomerExistsResponse>(model, endpoint);
            return response;


        }

        /// <summary>
        /// GetOrganizationByOrganizationName. returns the orgainization full object
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public async Task<Organization> GetOrganizationByOrganizationName(string organizationName)
        {
            var endpoint = $"employer/organization/name/{organizationName}/";
            var response = await aPiService.Get<Organization>(endpoint);
            return response;


        }

        public async Task<dynamic> GetWeatherFromOpenAPI()
        {
           // var endpoint = "https://samples.openweathermap.org/data/2.5/weather?q=London,uk&appid=b6907d289e10d714a6e88b30761fae22";
            var endpoint = "";
            var response = await aPiService.GetWithBaseURL<dynamic>(endpoint);
            return response;
        }
    }
}
