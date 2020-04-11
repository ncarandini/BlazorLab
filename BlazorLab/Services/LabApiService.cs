using BlazorLab.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorLab.Services
{
    public class LabApiService
    {
        private HttpClient httpClient;

        public LabApiService(HttpClient client)
        {
            this.httpClient = client;
        }

        public async Task<List<LabImage>> GetImagesAsync()
        {
            List<LabImage> images = new List<LabImage>();

            try
            {
                var response = await httpClient.GetAsync("api/image");
                response.EnsureSuccessStatusCode();
                using var responseContent = await response.Content.ReadAsStreamAsync();
                {
                    images = await JsonSerializer.DeserializeAsync<List<LabImage>>(responseContent);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Errore: {ex.Message}");
            }

            return images;
        }

        public async Task<LabImage> PostImageAsync(LabImage image)
        {
            try
            {
                string payload = JsonSerializer.Serialize(image);
                var content = new StringContent(payload, Encoding.Default, "application/json");
                var response = await httpClient.PostAsync($"api/image", content);
                response.EnsureSuccessStatusCode();
                using var responseContent = await response.Content.ReadAsStreamAsync();
                {
                    image = await JsonSerializer.DeserializeAsync<LabImage>(responseContent);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Errore: {ex.Message}");
            }

            return image;
        }
    }
}
