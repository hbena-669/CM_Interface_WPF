using Interface_WPF.Dtos;
using Interface_WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Interface_WPF.Services
{
    public class AuthApi : IAuthApi
    {
        private readonly HttpClient _http;

        private static readonly JsonSerializerOptions _jsonOptions =
            new() { PropertyNameCaseInsensitive = true };

        public AuthApi(HttpClient http)
        {
            _http = http;
        }

        public async Task<LoginResponseDto> LoginAsync(string login, string password)
        {
            try
            {
                var payload = new { login, password };

                var content = new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json");

                var response = await _http.PostAsync("auth/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Result = null,
                        ErrorMessage = "Identifiants invalides"
                    };
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<LoginResultDto>(json, _jsonOptions);

                return new LoginResponseDto
                {
                    Success = true,
                    Result = result,
                    ErrorMessage = string.Empty
                };
            }
            catch (HttpRequestException ex)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Result = null,
                    ErrorMessage = "Erreur de connexion au serveur"
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Result = null,
                    ErrorMessage = "Une erreur inattendue s'est produite"
                };
            }
        }

        public async Task<LoginResultDto> LoginAsync_old(string login, string password)
        {
            var payload = new
            {
                login,
                password
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");
            var c = content.ReadAsStringAsync();

            var response = await _http.PostAsync("auth/login", content);

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Login invalide");

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<LoginResultDto>(json, _jsonOptions)!;
        }

        public async Task<UserContextDto> GetContextAsync()
        {
            
            var response = await _http.GetAsync("auth/context");

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Token invalide");

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserContextDto>(json, _jsonOptions)!;
        }

        public void SetToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
