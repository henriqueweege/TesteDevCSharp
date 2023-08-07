using Infrastructure.Services;

namespace Infrastructure.IntegrationTests
{
    public class HttpServices_IntegrationTests
    {

        public string UrlApi { get; set; }
        public HttpServices_IntegrationTests()
        {
            UrlApi = "https://jsonmock.hackerrank.com/api/football_matches";
        }


        [Fact]
        public void GivenCorrectInput_HttpServices_ShouldReturnData()
        {
            //arrange
            var team = "Galatasaray";
            var year = 2015;

            //act
            var response = HttpServices.FetchFootBallMatchesData($"{UrlApi}?year={year}&team1={team}");

            //assert

            Assert.True(response.data.Count() > 0);
        }
    }
}