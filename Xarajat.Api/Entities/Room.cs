namespace Xarajat.Api.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public int AdminId { get; set; }
    public RoomStatus Status { get; set; }
    public User Admin { get; set; }
}
