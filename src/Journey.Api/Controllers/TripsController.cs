using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        try
        {
            var useCase = new RegisterTripsUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        } catch (JourneyException e)
        {
            return BadRequest(e.Message);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripsUseCase();
        
        var result = useCase.Execute();

        return Ok(result);
    }
}
