using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OptionsPattern.Models;

namespace OptionsPattern.Controllers
{
    public class HomeController : Controller
    {

        private readonly MyOptions _options;
        private readonly MyOptionsWithDelegateConfig _optionsWithDelegateConfig;
        private readonly MySubOptions _subOptions;
        public HomeController(
    IOptions<MyOptions> optionsAccessor,
    IOptions<MyOptionsWithDelegateConfig> optionsAccessorWithDelegateConfig,
      IOptions<MySubOptions> subOptionsAccessor,
    IOptionsSnapshot<MyOptions> snapshotOptionsAccessor,
    IOptionsSnapshot<MyOptions> namedOptionsAccessor)
        {
            _options = optionsAccessor.Value;
            _optionsWithDelegateConfig = optionsAccessorWithDelegateConfig.Value;
            _subOptions = subOptionsAccessor.Value;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {

            var option1 = _options.Option1;
            var option2 = _options.Option2;
            var data = $"option1 = {option1}, option2 = {option2}";
            ViewBag.Data = data;

            var delegate_config_option1 = _optionsWithDelegateConfig.Option1;
            var delegate_config_option2 = _optionsWithDelegateConfig.Option2;
            var configBydelegate =
                $"delegate_option1 = {delegate_config_option1}, " +
                $"delegate_option2 = {delegate_config_option2}";
            ViewBag.Data2 = configBydelegate;

            var subOption1 = _subOptions.SubOption1;
            var subOption2 = _subOptions.SubOption2;
            var data3 = $"subOption1 = {subOption1}, subOption2 = {subOption2}";
            ViewBag.Data3 = data3;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
