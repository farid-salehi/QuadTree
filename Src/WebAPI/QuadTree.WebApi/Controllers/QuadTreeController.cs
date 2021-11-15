using Microsoft.AspNetCore.Mvc;
using QuadTree.Application.Interfaces;
using QuadTree.Domain.DataTransferObjects;
using QuadTree.Domain.Models;

namespace QuadTree.WebApi.Controllers
{
    [Route("API/[Controller]/[action]")]
    public class QuadTreeController : Controller
    {
        private readonly IQuadTreeService _quadTreeService;

        public QuadTreeController(IQuadTreeService quadTreeService)
        {
            _quadTreeService = quadTreeService;
        }

        [HttpGet]
        public  IActionResult Search(double latitude, double longitude, int distance, int maxResult)
        {
            var result =
                 _quadTreeService.GetLocations(
                    new Location(latitude, longitude)
                    , distance
                    , maxResult);
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Insert([FromBody]InputLocationDto location)
        {
            var result =
                _quadTreeService.Insert(
                   location);
            return Ok(result);
        }

    }
}
