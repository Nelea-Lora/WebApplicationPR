using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplicationPR.Models;

namespace WebApplicationPR.Controllers
{
    [Route("api/user/recommendations")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public RecommendationController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRecommendations(int userId)
        {
            
            var response = await _httpClient.CreateClient("RecService").GetAsync($"api/recommendations/{userId}");
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var recommendations = await response.Content.ReadAsStringAsync();
            return Content(recommendations, "application/json");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetRecommendations()
        {
            var response = await _httpClient.CreateClient("RecService").GetAsync($"api/recommendations/");
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var recommendations = await response.Content.ReadAsStringAsync();
            return Content(recommendations, "application/json");
        }
        
        [HttpPost]
        public async Task<IActionResult> AddRecommendation([FromBody] RecommendationDto recommendationDto)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(recommendationDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.CreateClient("RecService").PostAsync("api/recommendations", jsonContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            return CreatedAtAction(nameof(GetUserRecommendations), new { userId = recommendationDto.Id }, recommendationDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecommendation( int id, [FromBody] RecommendationDto updatedRecommendationDto)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(updatedRecommendationDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.CreateClient("RecService").PutAsync($"api/recommendations/{id}", jsonContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            return Ok(updatedRecommendationDto);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecommendation( int id)
        {
            var response = await _httpClient.CreateClient("RecService")
                .DeleteAsync($"api/recommendations/{id}");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            return NoContent();
        }
    }
}
