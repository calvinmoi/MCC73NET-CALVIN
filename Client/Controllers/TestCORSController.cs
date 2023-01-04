using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[ApiController]
[Route("[controller]")]

public class TestCORSController : Controller
{


  public IActionResult Index()
  {
    return View();
  }
}