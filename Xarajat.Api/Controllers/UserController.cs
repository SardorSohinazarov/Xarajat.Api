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
    public List<Entities.User> GetUsers()
    {
        return _xarajatDbContext.Users.ToList();
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
    public List<Entities.User> AddUser(CreateUserModel userModel)
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
        return _xarajatDbContext.Users.ToList();
    }

    [HttpDelete]
    public List<Entities.User> DeleteUser(int id)
    {
        var deleteUser = _xarajatDbContext.Users.FirstOrDefault(x => x.Id == id);
        if (deleteUser is not null)
        {
            _xarajatDbContext.Users.Remove(deleteUser);
            _xarajatDbContext.SaveChanges();
        }
        return _xarajatDbContext.Users.ToList();
    }

    [HttpPut]
    public IActionResult UpdateUser(int id, UpdateUserModel updateuser)
    {
        var user = _xarajatDbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user is null)
            return NotFound();

        user.Email = updateuser.Email;
        user.Name = updateuser.Name;
        user.Phone = updateuser.Phone;

        _xarajatDbContext.SaveChanges();

        return Ok(_xarajatDbContext.Users.ToList());
    }

}
