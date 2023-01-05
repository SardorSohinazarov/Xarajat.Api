namespace Xarajat.Api.Entities;

public class Outlay
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }

    public int UserId { get; set; }
    public int RoomId { get; set; }
}
