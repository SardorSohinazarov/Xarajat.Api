namespace Xarajat.Api.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedTime { get; set; }

    public int? RoomId { get; set; }
}