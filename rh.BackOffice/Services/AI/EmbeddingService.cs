using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace rh.BackOffice.Services.AI
{
    public class EmbeddingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EmbeddingService> _logger;
        private readonly string _ollamaUrl;

        public EmbeddingService(IConfiguration configuration, ILogger<EmbeddingService> logger)
        {
            _logger = logger;
            _ollamaUrl = configuration["Ollama:Url"] ?? "http://localhost:11434";

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public async Task<float[]> GetEmbeddingAsync(string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    throw new ArgumentException("Le texte ne peut pas être vide.", nameof(text));

                var payload = new
                {
                    model = "nomic-embed-text",
                    prompt = text
                };

                var jsonPayload = JsonSerializer.Serialize(payload);
                _logger.LogInformation("Envoi de la requête à Ollama: {Payload}", jsonPayload);

                var content = new StringContent(
                    jsonPayload,
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync($"{_ollamaUrl}/api/embeddings", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Réponse Ollama ({StatusCode}): {Response}",
                    response.StatusCode, responseContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ollama API error ({response.StatusCode}): {responseContent}");
                }

                using var doc = JsonDocument.Parse(responseContent);
                var embedding = doc.RootElement.GetProperty("embedding");

                float[] vector = new float[embedding.GetArrayLength()];
                for (int i = 0; i < vector.Length; i++)
                {
                    vector[i] = (float)embedding[i].GetDouble();
                }

                _logger.LogInformation("Vecteur généré avec {Dimensions} dimensions", vector.Length);
                return vector;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération de l'embedding pour le texte: {Text}",
                    text?.Substring(0, Math.Min(100, text?.Length ?? 0)));
                throw;
            }
        }
    }
}