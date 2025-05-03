using System.Net;
using Microsoft.AspNetCore.Mvc;
using WhoseTurn.Managers;
using WhoseTurn.Models;

namespace WhoseTurn.Controllers;

[ApiController]
[Route("[controller]")]
public class DeterminantController : ControllerBase
{
    private readonly ILogger<DeterminantController> _logger;
    private readonly IDeterminantManager _determinantManager;

    public DeterminantController(ILogger<DeterminantController> logger, IDeterminantManager determinantManager)
    {
        _logger = logger;
        _determinantManager = determinantManager;
    }

    [HttpPost]
    public async Task<IActionResult> Determine([FromBody] OrderRequestModel request)
    {
        try
        {
            if (request.Persons.Count <= 1 || request.Persons.Sum(x => x.ItemOrderedAmount) <= 0)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Amount or people cannot be less than 0 or 1 respectively");
            }

            return Ok(await _determinantManager.DetermineWhoPays(request));
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error Occured when processing request, see logs for details");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        try
        {            
            return Ok(await _determinantManager.GetAllPersons());
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, "Error Occured when processing request, see logs for details");
        }
    }
}
