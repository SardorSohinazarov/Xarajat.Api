using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xarajat.Api.Data;
using Xarajat.Api.Entities;
using Xarajat.Api.Helpers;
using Xarajat.Api.Models;

namespace Xarajat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class RoomsController : ControllerBase
{
    private readonly XarajatDbContext _xarajatDbContext;

    public RoomsController(XarajatDbContext xarajatDbContext)
    {
        this._xarajatDbContext = xarajatDbContext;
    }

    [HttpGet]
    public IActionResult GetRooms()
    {
        return Ok(_xarajatDbContext.Rooms
            .Include(room => room.Admin)
            .ToList().Select(room=>ConvertToRoomModel(room))
            .ToList()
        );
    }
    [HttpGet("{id}")]
    public IActionResult GetRoom(int id)
    {
        var room = _xarajatDbContext.Rooms.Include(r => r.Admin).FirstOrDefault(room => room.Id == id);
        if (room is null)
            return NotFound();

        var getRoomModel = ConvertToRoomModel(room);

        return Ok(getRoomModel);
    }

    [HttpPost]
    public IActionResult AddRoom(CreateRoomModel createRoomModel)
    {
        var room = new Room()
        {
            Name = createRoomModel.Name,
            Status = RoomStatus.Created,
            Key = RandomGenerator.GetRandomString(),
            AdminId = 1 // login bo'p turgan user
        };
        _xarajatDbContext.Add(room);
        _xarajatDbContext.SaveChanges();
        return Ok(ConvertToRoomModel(room));
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
        return Ok(ConvertToRoomModel(room));
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

    private GetRoomModel ConvertToRoomModel(Room room)
    {
        return new GetRoomModel
        {
            Id = room.Id,
            Name = room.Name,
            Key = room.Key,
            Status = room.Status,
            Admin = room.Admin is null ? null : ConvertToUserModel(room.Admin),
        };
    }

    private GetUser ConvertToUserModel(User user)
    {
        return new GetUser
        {
            Id = user.Id,
            Name = user.Name
        };
    }

    [HttpGet("{id}/users")]
    public IActionResult GetRoomUsers(int id)
    {
        var room = _xarajatDbContext.Rooms
            .Include(r => r.Users)
            .FirstOrDefault(r => r.Id == id);

        return Ok(room.Users);
    }
}
