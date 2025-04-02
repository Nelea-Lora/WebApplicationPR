using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using TaskModel = WebApplicationPR.Models.Task;

namespace WebApplicationPR.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public TaskController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            var client = _httpClient.CreateClient("TaskService");

            var response = await client.GetAsync("");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var tasks = await response.Content.ReadFromJsonAsync<List<TaskModel>>();

            return Ok(tasks);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTask(Guid id)
        {
            var response = await _httpClient.CreateClient("TaskService").GetAsync($"{id}");
            var task = await response.Content.ReadFromJsonAsync<TaskModel>();
            if (task == null)
                return NotFound("Задача не найдена.");
            return Ok(task);
        }
        
        [HttpPost]
        public async Task<ActionResult<TaskModel>> AddTask([FromBody] TaskModel task)
        {
            var request = new
            {
                task.Title,
                task.Description,
                task.IsCompleted
            };
            
            Console.WriteLine(JsonSerializer.Serialize(request));

            var response = await _httpClient
                .CreateClient("TaskService")
                .PostAsJsonAsync("", request);

            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, responseText);

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskModel task)
        {
            var request = new
            {
                task.Title,
                task.Description,
                task.IsCompleted
            };
            var response = await _httpClient
                .CreateClient("TaskService")
                .PutAsJsonAsync($"{id}", request);

            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, responseText);

            return Ok(new
            {
                Message = "Задача успешно обновлена",
                Updated = task
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var response = await _httpClient
                .CreateClient("TaskService")
                .DeleteAsync($"{id}");

            var responseText = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, responseText);
            return Ok(new
            {
                Message = "Задача успешно удалена",
                DeletedTaskId = id
            });
        }

    }
}
