using FunctionalTests.ModelTest;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using UsersTBC.Application.Models;
using UsersTBC.Domain.Enums;
using UsersTBC.WebAPI.Helpers;
using Xunit;

namespace FunctionalTests.UserEndPoints
{
    [Collection("Sequential")]
    public class GetByIdEndpoint : IClassFixture<BaseFixture>
    {
        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public GetByIdEndpoint(BaseFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsItemGivenValidId()
        {
            
            var response = await Client.GetAsync("/v1/Users/8");
            var k3 = response.EnsureSuccessStatusCode().StatusCode.ToString();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var k = new GenericResponseWithData<UserResponseModel>();
            var model = JsonSerializer.Deserialize<UserResponseTest> (stringResponse, _jsonOptions);

            Assert.NotEqual("", k3);
            //Assert.Equal("string", model.data.FirstName);
        }

      
    }
}
