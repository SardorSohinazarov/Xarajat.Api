using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xarajat.Api.Entities;
using Xarajat.Api.Models;

namespace Xarajat.Api.Controllers;

public partial class RoomsController
{
    [HttpPost("{roomId}/outlays")]
    public IActionResult AddOutlay(int roomId , CreateOutlayModel createOutlayModel)
    {
        var outlay = new Outlay
        {
            Description = createOutlayModel.Description,
            Cost = createOutlayModel.Cost,
            UserId = createOutlayModel.UserId,
            RoomId = roomId,    
        };
        _xarajatDbContext.Outlays.Add(outlay);
        _xarajatDbContext.SaveChanges();
        return Ok(outlay);
    }

    [HttpGet("{roomId}/outlays")]
    public IActionResult GetRoomOutlaysByRoomId(int roomId)
    {
        var outlays = _xarajatDbContext.Outlays
            .Where(outlay => outlay.RoomId == roomId)
            .ToList();

        return Ok(outlays);
    }

    [HttpGet("{roomId}/outlays/calculate")]
    public IActionResult CalculateRoomOutlaysByRoomId(int roomId)
    {
        var room = _xarajatDbContext.Rooms
            .Include(room => room.Users)
            .Include(room => room.Outlays)
            .FirstOrDefault(room => room.Id == roomId);

        if(room is null)
            return NotFound();

        var calculate = new CalculateRoomOutlays
        {
            UserCount = room.Users.Count(),
            TotalCost = room.Outlays.Sum(outlay => outlay.Cost)
        };

        return Ok(calculate);
    }

}
