using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcRatings.Models;

    public class ContextDb : DbContext
    {
        public ContextDb (DbContextOptions<ContextDb> options)
            : base(options)
        {
        }

        public DbSet<MvcRatings.Models.Rating> Rating { get; set; } = default!;
        public DbSet<MvcRatings.Models.Album> Album { get; set; } = default!;
        public DbSet<MvcRatings.Models.Artist> Artist { get; set; } = default!;
        public DbSet<MvcRatings.Models.Song> Song { get; set; } = default!;
        public DbSet<MvcRatings.Models.User> User { get; set; } = default!; 
        

    }
