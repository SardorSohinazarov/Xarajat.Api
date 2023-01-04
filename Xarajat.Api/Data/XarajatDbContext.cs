using Microsoft.EntityFrameworkCore;
using Xarajat.Api.Entities;

namespace Xarajat.Api.Data
{
    public class XarajatDbContext : DbContext
    {
        public XarajatDbContext(DbContextOptions<XarajatDbContext> option)
            :base(option)
        {

        }
        public DbSet<User> Users {get; set;}
        public DbSet<Room> Rooms {get; set;}
    }
}
