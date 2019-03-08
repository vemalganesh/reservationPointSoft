using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using PsReservationPortal.Models;


namespace PsReservationPortal.MessageHandlers
{
    public class ApiKeyMessageHandler : DelegatingHandler
    {
        List<ApiKeyModel> CompanywithKeys = new List<ApiKeyModel>();
        private ApplicationDbContext _context;

        public ApiKeyMessageHandler()
        {
            _context = new ApplicationDbContext();
        }


        //public List<ApiKeyModel> Get()
        //{
            
        //    //ListOfOutlets = ListOfOutlets.Company_Id()

        //    return CompanywithKeys;
        //}
       

        //var CompanyValidation = _context.ApiKey.Where(x => x.Company_Id == id).ToList();
        //private const string APIKeyToCheck = "5544322090JII90";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            
            bool validkey = false;
            IEnumerable<string> requestHeaders;
            var checkApiKeyExist = httpRequestMessage.Headers.TryGetValues("APIKey", out requestHeaders);

            if (checkApiKeyExist)
            {
                CompanywithKeys = _context.ApiKey.Where(x => x.ApiKey == requestHeaders.FirstOrDefault()).ToList();
                if (CompanywithKeys.Count > 0)
                {
                    validkey = true;
                }

            }

            if (!validkey)
            {
                return httpRequestMessage.CreateResponse(HttpStatusCode.Forbidden, "Invalid Api Key");
            }

            var response = await base.SendAsync(httpRequestMessage, cancellationToken);
            return response;

        }

    }
}