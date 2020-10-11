using Coolpc_PriceTracker.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coolpc_PriceTracker.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90 Safari/537.36 Edg/90");
        }

        private static readonly HttpClient httpClient = new HttpClient();
        [ResponseCache(Duration =600,Location =ResponseCacheLocation.Any,NoStore =false)]
        public async Task<IActionResult> Index()
        {
            List<GitHubRepoResponse> gitHubRepoResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GitHubRepoResponse>>(await httpClient.GetAsync("https://api.github.com/repos/PH-68/Coolpc-PriceTracker.Data/contents/JSON-Data").Result.Content.ReadAsStringAsync());
            List<ItemListModel.ItemClasses> list = new List<ItemListModel.ItemClasses>();
            ItemListModel.ItemClasses itemListModel = new ItemListModel.ItemClasses();
            Parallel.For(0, gitHubRepoResponse.Count, async i =>
              {
                  itemListModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemListModel.ItemClasses>(await httpClient.GetAsync(gitHubRepoResponse[i].download_url).Result.Content.ReadAsStringAsync());
                  list.Add(itemListModel);
              });
            itemListModel = null;
            ViewBag.GitHubResponse = list;
            gitHubRepoResponse.Clear();
            //list.Clear();
            //System.GC.Collect();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}