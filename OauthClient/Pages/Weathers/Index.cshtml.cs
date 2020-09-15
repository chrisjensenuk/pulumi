using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using OauthClient.Models;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using Microsoft.Identity.Client;

namespace OauthClient.Pages.Weathers
{
    public class IndexModel : PageModel
    {
        private ITokenAcquisition tokenAcquisition { get; set; }

        public IndexModel(ITokenAcquisition tokenAcquisition)
        {
            this.tokenAcquisition = tokenAcquisition;
        }

        public IEnumerable<Weather> Weathers  { get; set; }

        public async Task OnGet()
        {
            var client = new HttpClient();

            try
            {
                //APIM OAuth 2.0
                string scopes = "api://{scope}/.default";
                string token = await tokenAcquisition.GetAccessTokenForAppAsync(scopes);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Weathers = await client.GetFromJsonAsync<Weather[]>("https://{some website}.azure-api.net/");
            }
            catch(Exception ex)
            {

            }

        }
    }
}