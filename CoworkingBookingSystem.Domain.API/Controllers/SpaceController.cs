using CoworkingBookingSystem.Domain.Commands;
using CoworkingBookingSystem.Domain.Commands.SpaceCommands;
using CoworkingBookingSystem.Domain.Entities;
using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Infra.Repositories;
using CoworkingBookingSystem.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoworkingBookingSystem.Domain.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class SpaceController : ControllerBase
{
    [Route("GetById/{spaceId}")]
    [HttpGet]
    public SpaceEntity GetSpaceById([FromServices] ISpaceRepository spaceRepository,Guid spaceId)
    {
        return spaceRepository.GetSpaceById(spaceId);
    }

    [Route("GetByName/{spaceName}")]
    [HttpGet]
    public SpaceEntity GetSpaceByName([FromServices] ISpaceRepository spaceRepository, string spaceName)
    {
        return spaceRepository.GetSpaceByName(spaceName);
    }
    
    [Route("")]
    [HttpGet]
    public IEnumerable<SpaceEntity> GetSpaces([FromServices]ISpaceRepository spaceRepository)
    {
        return spaceRepository.GetAllSpaces();
    }
    
    [Route("")]
    [HttpPost]
    public GenericCommandResult CreateSpace([FromBody]CreateSpaceCommand command, [FromServices]SpaceHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }

    [Route("")]
    [HttpPut]
    public GenericCommandResult UpdateSpace([FromBody] UpdateSpaceCommand command, [FromServices] SpaceHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [Route("")]
    [HttpDelete]
    public GenericCommandResult DeleteSpace([FromBody] DeleteSpaceCommand command, [FromServices] SpaceHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [Route("AddRoomToSpace")]
    [HttpPost]
    public GenericCommandResult AddRoomToSpace([FromBody]AddRoomToSpaceCommand command, [FromServices]SpaceHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [Route("UpdateRoomToSpace")]
    [HttpPut]
    public GenericCommandResult UpdateRoomToSpace([FromBody] UpdateRoomToSpaceCommand command, [FromServices] SpaceHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [Route("DeleteRoomFromSpace")]
    [HttpDelete]
    public GenericCommandResult DeleteRoomFromSpace([FromBody] RemoveRoomFromSpaceCommand command, [FromServices] SpaceHandler handler)
    {
        return (GenericCommandResult)handler.Handle(command);
    }
}