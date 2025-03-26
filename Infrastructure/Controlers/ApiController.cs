using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controlers;

public class ApiController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}