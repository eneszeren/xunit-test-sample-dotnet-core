using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace sample_service_test.Mock.Helper
{
    public class MockHttpClient : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient();
        }
    }
}
