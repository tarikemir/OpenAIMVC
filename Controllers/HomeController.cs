using Microsoft.AspNetCore.Mvc;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using System.Diagnostics;
using OpenAI.Interfaces;
using YetGenAkbankJump.MVCClient.Models;

namespace YetGenAkbankJump.MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOpenAIService _openAiService;

        public HomeController(ILogger<HomeController> logger, IOpenAIService openAiService)
        {
            _logger = logger;
            _openAiService = openAiService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index( HomeIndexViewModel viewModel)
        {

            var imageResult = await _openAiService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = viewModel.Prompt,
                N = viewModel.ImageCount,
                Size = StaticValues.ImageStatics.Size.Size512,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "MrT"
            });

            List<string> urls;

            if (imageResult.Successful)
            {
               urls = imageResult.Results.Select(r => r.Url).ToList();
            }

            return View();
        }

        [HttpGet]
        public IActionResult SeriTarikGetir()
        {
            return View();
        }
        [HttpGet]
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