using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xarajat.Api.Data;
using Xarajat.Api.Models;

namespace Xarajat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly XarajatDbContext _xarajatDbContext;

    public UserController(XarajatDbContext xarajatDbContext)
    {
        this._xarajatDbContext = xarajatDbContext;
    }
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_xarajatDbContext.Users.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _xarajatDbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public IActionResult AddUser(CreateUserModel userModel)
    {
        var user = new Entities.User()
        {
            Name = userModel.Name,
            Email = userModel.Email,
            Phone = userModel.Phone,
            CreatedTime = DateTime.Now
        };
        _xarajatDbContext.Users.Add(user);
        _xarajatDbContext.SaveChanges();
        return Ok(user);
    }

    [HttpDelete]
    public IActionResult DeleteUser(int id)
    {
        var deleteUser = _xarajatDbContext.Users.FirstOrDefault(x => x.Id == id);
        if (deleteUser is null)
            return NotFound();

        _xarajatDbContext.Users.Remove(deleteUser);
        _xarajatDbContext.SaveChanges();

        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateUser(int id, UpdateUserModel updateUser)
    {
        var user = _xarajatDbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user is null)
            return NotFound();

        user.Email = updateUser.Email;
        user.Name = updateUser.Name;
        user.Phone = updateUser.Phone;

        _xarajatDbContext.SaveChanges();

        return Ok(user);
    }

    [HttpPost("join-room/{roomId}")]
    public IActionResult JoinRoom(int roomId , string key , int userId)
    {
        var room = _xarajatDbContext.Rooms.FirstOrDefault(r => r.Id == roomId);

        if (room == null || key != room.Key)
            return NotFound();

        var user = _xarajatDbContext.Users.FirstOrDefault(u => u.Id == userId);

        user.RoomId = roomId;
        _xarajatDbContext.SaveChanges();
        return Ok(user);
    }

}
