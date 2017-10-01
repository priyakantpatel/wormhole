using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using wormhole.models;
using wormhole.repository.Disk;
using Xunit;

namespace wormhole.repository.test
{
    public class ZTemp
    {
        [Fact(DisplayName = "ZTemp.test_a")]
        public async void test_a()
        {
            var swaggerUrl = "http://product-api-app.azurewebsites.net/swagger/v1/swagger.json";
            var httpClient = new HttpClient(new HttpClientHandler());

            var jsonString = await httpClient.GetStringAsync(swaggerUrl);

            jsonString.Should().NotBeNullOrWhiteSpace();
        }
    }
}
