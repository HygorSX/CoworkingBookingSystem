using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.ReservationCommands;
using CoworkingBookingSystem.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingBookingSystem.Domain.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ReservationController : ControllerBase
{
    [Route("")]
    [HttpPost]
    public GenericCommandResult CreateReservation([FromBody]CreateReservationCommand command, [FromServices]ReservationHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }

    [Route("")]
    [HttpPut]
    public GenericCommandResult UpdateReservation([FromBody] UpdateReservationTimeCommand command, [FromServices] ReservationHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [Route("MarkReservationAsCanceled")]
    [HttpPut]
    public GenericCommandResult UpdateReservation([FromBody] MarkReservationAsCanceledCommand command, [FromServices] ReservationHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [Route("MarkReservationAsCompleted")]
    [HttpPut]
    public GenericCommandResult UpdateReservation([FromBody] MarkReservationAsCompletedCommand command, [FromServices] ReservationHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [Route("MarkReservationAsReserved")]
    [HttpPut]
    public GenericCommandResult UpdateReservation([FromBody] MarkReservationAsReservedCommand command, [FromServices] ReservationHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
}