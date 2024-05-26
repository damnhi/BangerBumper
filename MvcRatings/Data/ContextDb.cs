using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BangerBumper.WebAPP.MVC.Models;

    public class ContextDb : DbContext
    {
        public ContextDb (DbContextOptions<ContextDb> options)
            : base(options)
        {
        }

        public DbSet<BangerBumper.WebAPP.MVC.Models.Rating> Rating { get; set; } = default!;
        public DbSet<BangerBumper.WebAPP.MVC.Models.Album> Album { get; set; } = default!;
        public DbSet<BangerBumper.WebAPP.MVC.Models.Artist> Artist { get; set; } = default!;
        public DbSet<BangerBumper.WebAPP.MVC.Models.Song> Song { get; set; } = default!;
        public DbSet<BangerBumper.WebAPP.MVC.Models.User> User { get; set; } = default!; 
        

    }
