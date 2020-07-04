using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MosulendWrapper.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MosulendWrapper.Services
{


 
    public class APIService: IAPiService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<APIService> logger;
        private readonly IHttpClientFactory iHttpClientFactory;

        public APIService(IConfiguration configuration, ILogger<APIService> logger, IHttpClientFactory iHttpClientFactory)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.iHttpClientFactory = iHttpClientFactory;
        }

        /// <summary>
        /// Generic post method for API calls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="endPoint"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<T> Post<T>(object model, string endPoint, string contentType = "application/json")
        {

            logger.LogInformation($"got to post in APIService");
            try
            {
                string json = string.Empty;

                var client = iHttpClientFactory.CreateClient("MosulendWrapper");
                string url =endPoint;
               
                json = JsonConvert.SerializeObject(model);
                logger.LogInformation($"got to post in APIService. json is {json}. url is {client.BaseAddress + url}");

                var stringContent = new StringContent(json, Encoding.UTF8, contentType);
                var message = await client.PostAsync(url, stringContent).ConfigureAwait(false);

                var content = await message.Content.ReadAsStringAsync();
                logger.LogInformation($"got to post in APIService. json is {json}. url is {url}. content is {content}");

                var response = JsonConvert.DeserializeObject<T>(content);


                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                throw new Exception(ex.Message);
            }

        }
        
        
        
        
        /// <summary>
        /// Generic get methods for api calls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endPoint"></param>
        /// <param name="actionPerformed"></param>
        /// <returns></returns>
        public async  Task<T> Get<T>(string endPoint, string actionPerformed)
        {
            string url = endPoint;

            var client = iHttpClientFactory.CreateClient("MosulendWrapper");

            logger.LogInformation($"got to get in APIService. url is {client.BaseAddress + url}");


            var res = await client.GetAsync(url).ConfigureAwait(false);         

            var content = res != null ? await res.Content.ReadAsStringAsync() : "";
            logger.LogInformation($"got to get in APIService. url is {url}. content is {content}");

            var deserialize = JsonConvert.DeserializeObject<T>(content);
            return deserialize;

        }



        /// <summary>
        /// Generic get methods for api calls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endPoint"></param>
        /// <param name="actionPerformed"></param>
        /// <returns></returns>
        public async Task<T> GetWithBaseURL<T>(string endPoint, string actionPerformed)
        {

            var client = iHttpClientFactory.CreateClient("OpenAPI");

            string url = "";
            url = endPoint;
            logger.LogInformation($"got to get in APIService. url is {client.BaseAddress + url}");

            var res = await client.GetAsync(url).ConfigureAwait(false);

            var content = res != null ? await res.Content.ReadAsStringAsync() : "";
            logger.LogInformation($"got to get in APIService. url is {url}. content is {content}");

            var deserialize = JsonConvert.DeserializeObject<T>(content);
            return deserialize;


        }

        }


    }

