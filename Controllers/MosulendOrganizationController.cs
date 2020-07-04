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
    public class MosulendOrganizationController : ControllerBase
    {
        private readonly ILogger<MosulendOrganizationController> logger;
        private readonly Interfaces.IDataService dataService;

        public MosulendOrganizationController(ILogger<MosulendOrganizationController> logger, Interfaces.IDataService dataService)
        {
            this.logger = logger;
            this.dataService = dataService;
        }

        [HttpGet]
        [Route("/GetOrganizationByOrganizationName/{organizationname}")]
        public async Task<IActionResult> GetOrganizationByOrganizationName(string organizationname)
        {
            logger.LogInformation($" got to the controller mosulend controller, action GetOrganizationByOrganizationName. parameter is {organizationname} ");

            var response =await dataService.GetOrganizationByOrganizationName(organizationname);
            logger.LogInformation($"responding to the request on mosulend controller, action GetOrganizationByOrganizationName. parameter is {organizationname}. response is {JsonConvert.SerializeObject(response)} ");
            return Ok(response);

        }


    }
}