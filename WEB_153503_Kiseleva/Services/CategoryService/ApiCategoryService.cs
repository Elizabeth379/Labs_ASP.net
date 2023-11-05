using System.Text.Json;
using System.Text;
using WEB_153503_Kiseleva.Domain.Models;
using WEB_153503_Kiseleva.Domain.Entities;

namespace WEB_153503_Kiseleva.Services.CategoryService
{
    public class ApiCategoryService: ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiCategoryService> _logger;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiCategoryService(HttpClient httpClient, ILogger<ApiCategoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            // подготовка URL запроса
            var urlString = new StringBuilder($"{_httpClient.BaseAddress?.AbsoluteUri}Categories/");

            // отправить запрос к API
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content
                        .ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<List<Category>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}"
                    };
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
            return new ResponseData<List<Category>>
            {
                Success = false,
                ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode}"
            };
        }
    }
}
