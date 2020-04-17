using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MVC
{
    public class APIService
    {
        public static HttpClient client = new HttpClient();
        static APIService()
        {
            client.BaseAddress = new Uri("http://localhost:63246/api/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}