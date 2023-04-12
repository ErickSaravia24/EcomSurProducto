using Microsoft.AspNetCore.Mvc;

namespace WebApiProducts.Controllers
{
    [ApiController]
    [Route("/")]

    public class DefaultController:ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Ecom Sur Productos";
        }

    }
}
