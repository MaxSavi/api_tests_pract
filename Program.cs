using System;
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
            await CreatePost(baseURL, new { title = "New Post", author = "New Author" });
            await CreateGame(baseURL, new { title = "CSGO" });
            await GetAllPosts(baseURL);
            await GetPostById(baseURL, 1);
            await UpdatePost(baseURL, 1, new { title = "Updated Post", author = "Updated Author" });
            await PartialUpdatePost(baseURL, 1, new { title = "Updated Post Title" });
            await DeletePost(baseURL, 2);
            await CreateBook(baseURL, new { author = "PETROV", book_title = "Yay" });
            await DeleteGame(baseURL, 2);
            await UpdateGame(baseURL, 1, new { name_of_game = "Tetris" });
            await UpdateBook(baseURL, 1, new { author = "VOVKIN", book_title = "Sun" });
            await CreateSubject(baseURL, new { name = "PE", teacher = "KUZNETSOV" });
            await UpdateSubject(baseURL, 2, new { name = "ENGlang", teacher = "SOROKIN" });
            await DeleteSubject(baseURL, 2);
            await DeleteBook(baseURL, 2);
        }

        static async Task CreatePost(string baseURL, object postData)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(postData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseURL}/posts", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Пост успешно создан: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task CreateGame(string baseURL, object gameData)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(gameData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseURL}/games", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Игра успешно создана: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task CreateBook(string baseURL, object bookData)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(bookData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseURL}/book", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Книга успешно создана: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task CreateSubject(string baseURL, object SubjectData)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(SubjectData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{baseURL}/subject", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Предмет успешно создан: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task GetAllPosts(string baseURL)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseURL}/posts");
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Все посты: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task GetPostById(string baseURL, int postId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseURL}/posts/{postId}");
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Пост с ID {postId}: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task UpdatePost(string baseURL, int postId, object postData)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(postData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{baseURL}/posts/{postId}", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Пост успешно обновлен: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task UpdateGame(string baseURL, int gameId, object gameData)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(gameData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{baseURL}/games/{gameId}", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Пост успешно обновлен: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task UpdateBook(string baseURL, int bookId, object bookData)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(bookData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{baseURL}/book/{bookId}", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Пост успешно обновлен: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task UpdateSubject(string baseURL, int bookId, object subjectData)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(subjectData);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{baseURL}/subject/{bookId}", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Предмет успешно обновлен: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task PartialUpdatePost(string baseURL, int postId, object partialData)
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

                Console.WriteLine($"Часть поста успешно обновлена: {await response.Content.ReadAsStringAsync()}");
            }
        }

        static async Task DeletePost(string baseURL, int postId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/posts/{postId}");
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Пост успешно удален: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task DeleteGame(string baseURL, int gameId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/games/{gameId}");
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Игра успешно удалена: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task DeleteSubject(string baseURL, int subjectId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/subject/{subjectId}");
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Предмет успешно удален: {await response.Content.ReadAsStringAsync()}");
            }
        }
        static async Task DeleteBook(string baseURL, int bookId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"{baseURL}/book/{bookId}");
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Предмет успешно удален: {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
