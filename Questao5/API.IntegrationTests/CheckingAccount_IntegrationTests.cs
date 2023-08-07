using API.ServicesForTests;
using Domain.Commands;
using Domain.Commands.CheckingAccountCommands;
using Domain.Enums;
using Domain.Models;
using Domain.Queries;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using Tools;

namespace API.IntegrationTests
{
    public class CheckingAccount_IntegrationTests
    {
        private HttpClient Client { get; set; }
        private string BaseUrl { get; set; }
        private Guid IdCheckingAccount { get; set; }
        public CheckingAccount_IntegrationTests()
        {
            Client = WebApi.Client;
            BaseUrl = "http://localhost:44395/CheckingAccount";
            IdCheckingAccount = Guid.Parse("b6bafc09-6967-ed11-a567-055dfa4a16c9");
        }

        [Fact]
        public void ExecuteTransaction_ShouldReturnOk()
        {
            //arrange

            var idTransaction = Guid.NewGuid();

            var url = $"{BaseUrl}/ExecuteTransaction"; 
            var stringContent = Helper<CheckingAccountModel>.CreateStringContent(new ExecuteTransactionCommand() 
            { 
                Id = idTransaction,
                IdCheckingAccount = IdCheckingAccount,
                Type = char.Parse(ETransactionType.Credit.ToDescription()),
                Value = 5
            });

            //act
            Task<HttpResponseMessage> response = Client.PostAsync(url, stringContent);
            response.Wait();


            //assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Fact]
        public void GetBalance_WithoutTransaction_ShouldReturnNoContent()
        {
            //arrange
            var url = $"{BaseUrl}/GetBalance?id={IdCheckingAccount}";

            //act
            Task<HttpResponseMessage> response = Client.GetAsync(url);
            response.Wait();


            //assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Fact]
        public void GetBalance_WithTransaction_ShouldReturnOk()
        {
            //arrange
            var idTransaction = Guid.NewGuid();

            var url = $"{BaseUrl}/ExecuteTransaction";
            var stringContent = Helper<CheckingAccountModel>.CreateStringContent(new ExecuteTransactionCommand()
            {
                Id = idTransaction,
                IdCheckingAccount = IdCheckingAccount,
                Type = char.Parse(ETransactionType.Credit.ToDescription()),
                Value = 5
            });

            Task<HttpResponseMessage> makeTransaction = Client.PostAsync(url, stringContent);
            makeTransaction.Wait();

             url = $"{BaseUrl}/GetBalance?id={IdCheckingAccount}";

            //act
            Task<HttpResponseMessage> response = Client.GetAsync(url);
            response.Wait();


            //assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.Result.StatusCode);
        }

    }
}