using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xarajat.Api.Data;
using Xarajat.Api.Entities;
using Xarajat.Api.Helpers;
using Xarajat.Api.Models;

namespace Xarajat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly XarajatDbContext _xarajatDbContext;

    public RoomsController(XarajatDbContext xarajatDbContext)
    {
        this._xarajatDbContext = xarajatDbContext;
    }

    [HttpGet]
    public IActionResult GetRooms()
    {
        return Ok(_xarajatDbContext.Rooms.Select(ConvertToGetRoomModel).ToList());
    }
    [HttpGet("{id}")]
    public IActionResult GetRoom(int id)
    {
        var room = _xarajatDbContext.Rooms.FirstOrDefault(room => room.Id == id);
        if (room is null)
            return NotFound();
        return Ok(ConvertToGetRoomModel(room));
    }

    [HttpPost]
    public IActionResult AddRoom(CreateRoomModel createRoomModel)
    {
        var room = new Room()
        {
            Name = createRoomModel.Name,
            Status = RoomStatus.Created,
            Key = RandomGenerator.GetRandomString(),
            //AdminId = ?
        };
        _xarajatDbContext.Add(room);
        _xarajatDbContext.SaveChanges();
        return Ok(ConvertToGetRoomModel(room));
    }

    [HttpPut]
    public IActionResult UpdateRoom(int id , UpdateRoomModel updateRoomModel)
    {
        var room = _xarajatDbContext.Rooms.FirstOrDefault(room => room.Id == id);
        if (room is null)
            return NotFound();

        room.Name = updateRoomModel.Name;
        room.Status = updateRoomModel.Status;

        _xarajatDbContext.SaveChanges();
        return Ok(ConvertToGetRoomModel(room));
    }
    
    [HttpDelete]
    public IActionResult DeleteRoom(int id)
    {
        var room = _xarajatDbContext.Rooms.FirstOrDefault(room => room.Id == id);
        if (room is null)
            return NotFound();

        _xarajatDbContext.Rooms.Remove(room);
        _xarajatDbContext.SaveChanges();
        return Ok();
    }

    private GetRoomModel ConvertToGetRoomModel(Room room)
    {
        return new GetRoomModel
        {
            Id = room.Id,
            Name = room.Name,
            Key = room.Key,
            Status = room.Status
        };
    }
}
