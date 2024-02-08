using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mom2.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mom2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {

        Games games = new();
        games.Setup();

        ViewBag.GameObj = games;


        return View(games.game);
    }

    [HttpGet]
    public IActionResult Add(){
        return View();
    }

    [HttpPost]
    public IActionResult AddGame(IFormCollection col){
        Games games = new();
        games.Setup();
        games.Name = col["Name"];
        games.Author = col["Author"];
        games.Year = Convert.ToInt32(col["Year"]);
        games.AddGame(games.Name, games.Author, games.Year);
        
        ViewBag.Name = games.Name;
        ViewBag.Author = games.Author;
        ViewBag.Year = games.Year;

        return View();
    }

    [HttpGet("Delete/{id}")]
    public IActionResult Delete(int id){
        Games games = new();
        games.Setup();
        games.DeleteGame(id);
        games.Save();
        
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
