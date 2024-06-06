using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcRatings.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MvcRatings.Data
{
    public class ContextDb : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ContextDb(DbContextOptions<ContextDb> options)
            : base(options)
        {
        }

        public DbSet<MvcRatings.Models.Rating> Rating { get; set; } = default!;
        public DbSet<MvcRatings.Models.Album> Album { get; set; } = default!;
        public DbSet<MvcRatings.Models.Artist> Artist { get; set; } = default!;
        public DbSet<MvcRatings.Models.Song> Song { get; set; } = default!;
        public DbSet<MvcRatings.Models.User> User { get; set; } = default!;


    }
}