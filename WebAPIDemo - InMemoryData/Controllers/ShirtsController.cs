using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Filters;
using WebAPIDemo.Filters.ActionFilters;
using WebAPIDemo.Filters.ExceptionFilters;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }
       
        [HttpPost]
        [Shirt_ValidateShirtUniqueFilter]   
        public IActionResult CreateShirt([FromBody]Shirt shirt)
        {
            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(
                nameof(GetShirtById), 
                new { id = shirt.ShirtId }, 
                shirt);
        }

        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(shirt);
        }

    }
}

/* For additional arguments passed to the endpoint 
            url: .../api/shirts/9/red
            [HttpGet("{id}/{color}")]
            public string GetShirtColorFromRoute(int id, [FromRoute] string color)
            {
                return $"Reading shirt: {id} color: {color}";
            }


            url: .../api/shirts/9?red
            [HttpGet("{id}")]
            public string GetShirtColorFromQuery(int id, [FromQuery] string color)
            {
                return $"Reading shirt: {id} color: {color}";
            }

            url: .../api/shirts/9
            header: key=color  value=red 
            [HttpGet("{id}")]
            public string GetShirtColorFromQuery(int id, [FromHeader(Name = "color")] string color)
            {
                return $"Reading shirt: {id} color: {color}";
            }

            body: { ShirtId: 1, ...}
            [HttpPost]
            public string CreateShirt([FromBody]Shirt shirt)
            {
                return "Creating a shirt";
            }

        */


/* Format if no [Route("[controller]")]
[HttpGet]
[Route("/shirts")]
public string GetShirts()
{
    return "Reading all shirts";
}

[HttpGet]
[Route("/shirts/{id}")]
public string GetShirtById(int id)
{
    return $"Reading shirt: {id}";
}

[HttpPost]
[Route("/shirts")]
public string CreateShirt()
{
    return "Creating a shirt";
}

[HttpPut]
[Route("/shirts/{id}")]
public string UpdateShirt(int id)
{
    return $"Updating shirt: {id}";
}

[HttpDelete]
[Route("/shirts/{id}")]
public string DeleteShirt(int id)
{
    return $"Deleting shirt: {id}";
}
*/