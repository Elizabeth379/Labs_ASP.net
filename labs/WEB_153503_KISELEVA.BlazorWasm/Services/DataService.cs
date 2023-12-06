using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;

namespace WEB_153503_KISELEVA.BlazorWasm.Services
{
    public class DataService : IDataService
    {
        private HttpClient _httpClient;
        private String _apiUri;
        private int _itemsPerPage;
        private JsonSerializerOptions _serializerOptions;
        private IAccessTokenProvider _accessTokenProvider;

        public DataService(HttpClient httpClient, IConfiguration configuration, IAccessTokenProvider accessTokenProvider)
        {
            _httpClient = httpClient;
            _apiUri = configuration.GetValue<String>("ApiUri")!;
            _itemsPerPage = configuration.GetValue<int>("ItemsPerPage");

            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _accessTokenProvider = accessTokenProvider;
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Product> ObjectsList { get; set; } = new List<Product>();

        public bool Success { get; set; }

        public string ErrorMessage { get; set; } = "";

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public event Action DataLoaded;

        public async Task GetCategoryListAsync()
        {
            var tokenRequest = await _accessTokenProvider.RequestAccessToken();
            if (tokenRequest.TryGetToken(out var token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Value);
                var urlString = new StringBuilder($"{_apiUri}categories/");

                var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        Categories = (await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions)).Data;
                        DataLoaded?.Invoke();

                    }
                    catch (JsonException ex)
                    {

                        Success = false;
                        ErrorMessage = $"Ошибка: {ex.Message}";
                        return;

                    }
                }


                Success = false;
                ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode}";
                return;
            }
            
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var tokenRequest = await _accessTokenProvider.RequestAccessToken();
            if (tokenRequest.TryGetToken(out var token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Value);
                var urlString = new StringBuilder($"{_apiUri}products/{id}");


                var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = await response.Content.ReadFromJsonAsync<ResponseData<Product>>(_serializerOptions);
                        DataLoaded?.Invoke();
                        return result.Data;

                    }
                    catch (JsonException ex)
                    {

                        Success = false;
                        ErrorMessage = $"Ошибка: {ex.Message}";
                        return null;
                    }
                }
                Success = false;
                ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode}";
                return null;
            }
            return null;
        }

        public async Task GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var tokenRequest = await _accessTokenProvider.RequestAccessToken();
            if (tokenRequest.TryGetToken(out var token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Value);
                var urlString = new StringBuilder($"{_apiUri}products/");

                if (categoryNormalizedName != null)
                {
                    urlString.Append($"{categoryNormalizedName}/");
                };
                if (pageNo > 1)
                {
                    urlString.Append($"page{pageNo}/");
                };
                if (!_itemsPerPage.Equals(3))
                {
                    urlString.Append($"size{_itemsPerPage}");
                }

                var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = (await response.Content.ReadFromJsonAsync<ResponseData<ProductListModel<Product>>>(_serializerOptions)).Data!;
                        ObjectsList = result.Items;
                        TotalPages = result.TotalPages;
                        CurrentPage = result.CurrentPage;
                        DataLoaded?.Invoke();

                    }
                    catch (JsonException ex)
                    {
                        Success = false;
                        ErrorMessage = $"Ошибка: {ex.Message}";
                        return;
                    }
                }

                Success = false;
                ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode}";
                return;
            }
                
        }
    }
}

