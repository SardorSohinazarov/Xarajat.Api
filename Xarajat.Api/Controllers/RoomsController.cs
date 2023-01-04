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
        var rooms = _xarajatDbContext.Rooms.Select(room => new GetRoomModel()
        {
            Id = room.Id,
            Name = room.Name,
            Key = room.Key,
            Status = room.Status
        }).ToList();
        return Ok(rooms);
    }
    [HttpGet("{id}")]
    public IActionResult GetRooms(int id)
    {
        var room = _xarajatDbContext.Rooms.FirstOrDefault(room => room.Id == id);
        if (room is null)
            return NotFound();
        return Ok(room);
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
        return Ok(room);
    }
}
