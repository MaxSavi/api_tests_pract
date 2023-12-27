using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Allure.Commons;
using Xunit;
namespace api_test
{
    class Program
    {
        static async Task Main()
        {
            string baseURL = "http://localhost:3000";
            string logFilePath = "C:\\Users\\Asus\\Desktop\\VS\\api_tests_pract\\log.txt";
            await CreatePost(baseURL, new { title = "New Post", author = "New Author" }, logFilePath);
            await CreateGame(baseURL, new { title = "CSGO" }, logFilePath);
            await GetAllPosts(baseURL, logFilePath);
            await GetPostById(baseURL, 1, logFilePath);
            await UpdatePost(baseURL, 1, new { title = "Updated Post", author = "Updated Author" }, logFilePath);
            await PartialUpdatePost(baseURL, 1, new { title = "Updated Post Title" }, logFilePath);
            await DeletePost(baseURL, 2, logFilePath);
            await CreateBook(baseURL, new { author = "PETROV", book_title = "Yay" }, logFilePath);
            await DeleteGame(baseURL, 2, logFilePath);
            await UpdateGame(baseURL, 1, new { name_of_game = "Tetris" }, logFilePath);
            await UpdateBook(baseURL, 1, new { author = "VOVKIN", book_title = "Sun" }, logFilePath);
            await CreateSubject(baseURL, new { name = "PE", teacher = "KUZNETSOV" }, logFilePath);
            await UpdateSubject(baseURL, 2, new { name = "ENGlang", teacher = "SOROKIN" }, logFilePath);
            await DeleteSubject(baseURL, 2, logFilePath);
            await DeleteBook(baseURL, 2, logFilePath);
        }
        static void LogToFile(string filePath, string requestType, System.Net.HttpStatusCode statusCode)
        {
            try
            {
                string logEntry = $"[{DateTime.Now}] {requestType} Request - Status Code: {statusCode}";

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
        static async Task CreatePost(string baseURL, object postData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(postData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseURL}/posts", content);
                response.EnsureSuccessStatusCode();

                LogToFile(logFilePath, "POST", response.StatusCode);

                Console.WriteLine($"Пост успешно создан: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task CreateGame(string baseURL, object gameData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(gameData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseURL}/games", content);
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "GAME", response.StatusCode);
                Console.WriteLine($"Игра успешно создана: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task CreateBook(string baseURL, object bookData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(bookData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseURL}/book", content);
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "BOOK", response.StatusCode);
                Console.WriteLine($"Книга успешно создана: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task CreateSubject(string baseURL, object SubjectData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(SubjectData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseURL}/subject", content);
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "SUBJECT", response.StatusCode);
                Console.WriteLine($"Предмет успешно создан: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task GetAllPosts(string baseURL, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseURL}/posts");
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "POST", response.StatusCode);
                Console.WriteLine($"Все посты: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task GetPostById(string baseURL, int postId, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseURL}/posts/{postId}");
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "POST", response.StatusCode);
                Console.WriteLine($"Пост с ID {postId}: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task UpdatePost(string baseURL, int postId, object postData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(postData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{baseURL}/posts/{postId}", content);
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "POST", response.StatusCode);
                Console.WriteLine($"Пост успешно обновлен: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task UpdateGame(string baseURL, int gameId, object gameData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(gameData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{baseURL}/games/{gameId}", content);
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "GAME", response.StatusCode);
                Console.WriteLine($"Пост успешно обновлен: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task UpdateBook(string baseURL, int bookId, object bookData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(bookData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{baseURL}/book/{bookId}", content);
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "BOOK", response.StatusCode);
                Console.WriteLine($"Пост успешно обновлен: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task UpdateSubject(string baseURL, int bookId, object subjectData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(subjectData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{baseURL}/subject/{bookId}", content);
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "SUBJECT", response.StatusCode);
                Console.WriteLine($"Предмет успешно обновлен: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task PartialUpdatePost(string baseURL, int postId, object partialData, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(partialData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{baseURL}/posts/{postId}")
                {
                    Content = content
                };

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "POST", response.StatusCode);
                Console.WriteLine($"Часть поста успешно обновлена: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task DeletePost(string baseURL, int postId, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/posts/{postId}");
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "POST", response.StatusCode);
                Console.WriteLine($"Пост успешно удален: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task DeleteGame(string baseURL, int gameId, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/games/{gameId}");
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "GAME", response.StatusCode);
                Console.WriteLine($"Игра успешно удалена: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task DeleteSubject(string baseURL, int subjectId, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/subject/{subjectId}");
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "SUBJECT", response.StatusCode);
                Console.WriteLine($"Предмет успешно удален: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task DeleteBook(string baseURL, int bookId, string logFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/book/{bookId}");
                response.EnsureSuccessStatusCode();
                LogToFile(logFilePath, "BOOK", response.StatusCode);
                Console.WriteLine($"Предмет успешно удален: {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
