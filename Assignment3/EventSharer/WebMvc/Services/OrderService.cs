using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models.OrderModels;

namespace WebMvc.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public OrderService(IOptionsSnapshot<AppSettings> settings, IHttpContextAccessor httpContextAccessor, IHttpClient httpClient, ILoggerFactory factory)
        {
            _remoteServiceBaseUrl = $"{settings.Value.OrderUrl}/api/v1/orders";
            _settings = settings;
            _httpContextAccessor = httpContextAccessor;
            _apiClient = httpClient;
            _logger = factory.CreateLogger<OrderService>();
        }

        public async Task<int> CreateOrder(Order order)
        {
            var token = await GetUserTokenAsync();
            var addNewOrderUrl = ApiPaths.Order.AddNewOrder(_remoteServiceBaseUrl);
            _logger.LogDebug("OrderUri " + addNewOrderUrl);
            var response = await _apiClient.PostAsync(addNewOrderUrl, order, token);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error creating order, try again later.");
            }

            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            _logger.LogDebug("response " + jsonString);
            dynamic data = JObject.Parse(jsonString.Result);
            string value = data.orderId;
            return Convert.ToInt32(value);

        }

        public async Task<Order> GetOrder(string orderId)
        {
            var token = await GetUserTokenAsync();
            var getOrderUri = ApiPaths.Order.GetOrder(_remoteServiceBaseUrl, orderId);
            var dataString = await _apiClient.GetStringAsync(getOrderUri, token);
            _logger.LogInformation("Data String: " + dataString);
            var response = JsonConvert.DeserializeObject<Order>(dataString);

            return response;
        }

        public async Task<List<Order>> GetOrders()
        {
            var token = await GetUserTokenAsync();
            var allOrdersUri = ApiPaths.Order.GetOrders(_remoteServiceBaseUrl);
            var dataString = await _apiClient.GetStringAsync(allOrdersUri, token);
            var response = JsonConvert.DeserializeObject<List<Order>>(dataString);

            return response;
        }

        async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            return await context.GetTokenAsync("access_token");
        }
    }
}
