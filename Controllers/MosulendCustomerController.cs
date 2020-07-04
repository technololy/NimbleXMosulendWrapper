using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MosulendWrapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MosulendCustomerController : ControllerBase
    {
        private readonly ILogger<MosulendCustomerController> logger;
        private readonly Interfaces.IDataService dataService;

        public MosulendCustomerController(ILogger<MosulendCustomerController> logger, Interfaces.IDataService dataService)
        {
            this.logger = logger;
            this.dataService = dataService;
        }
        /// <summary>
        /// pass phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/GetCustomersExistenceByPhone/{phone}")]
        public async Task<IActionResult> GetCustomersExistenceByPhone(string phone)
        {
            logger.LogInformation($"got to the controller mosulendcustomer controller, action GetCustomersExistenceByPhone. parameter is {phone} ");

            var response = await dataService.GetCustomersExistenceByPhone(phone);
            logger.LogInformation($"responding to the request on mosulendcustomer controller, action GetCustomersExistenceByPhone. parameter is {phone}. response is {JsonConvert.SerializeObject(response)} ");
            return Ok(response);
        }
    }
}