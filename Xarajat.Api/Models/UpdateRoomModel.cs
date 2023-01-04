using Xarajat.Api.Entities;

namespace Xarajat.Api.Models;

public class UpdateRoomModel
{
    public string Name { get; set; }
    public RoomStatus Status { get; set; }
}
