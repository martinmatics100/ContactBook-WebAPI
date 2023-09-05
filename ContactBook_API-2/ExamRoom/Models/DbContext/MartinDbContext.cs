using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactBook_API.Models.DbContext
{
    public class MartinDbContext : IdentityDbContext<User>
    {
        public MartinDbContext(DbContextOptions<MartinDbContext> options) : base(options)
        {

        }

    }
}
