using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using StudyAssignmentManager.Domain;

namespace StudyAssignmentManager.Client.Services
{
    public static class UserService
    {
        public static UserData userData { get; set; }
        private static readonly HttpClient client;
        private static readonly JsonSerializerOptions options;

        static UserService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/api/")
            };
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
        
        public static async Task LoginAsTeacher()
        {
            var authRequestModel = new AuthenticationRequest
            {
                Email = "vasilyev_alexey@sam.ru",
                Password = "asd",
            };

            await Login(authRequestModel);
        }
        
        public static async Task LoginAsStudent()
        {
            var authRequestModel = new AuthenticationRequest
            {
                Email = "ni_ma@sam.ru",
                Password = "asd",
            };

            await Login(authRequestModel);
        }
        
        public static async Task Login(AuthenticationRequest authRequestModel)
        {
            var response = await client.PostAsJsonAsync("login", authRequestModel);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Не удалось выполнить вход");
            }
            
            var authResponse = await response.Content.ReadFromJsonAsync<UserData>();

            userData = new UserData
            {
                Id = authResponse.Id,
                Email = authResponse.Email,
                FullName = authResponse.FullName,
                Role = authResponse.Role,
            };
        }
    }
}