using BandAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.DbContexts
{
    public class BandAlbumContext : DbContext
    {
        public BandAlbumContext(DbContextOptions<BandAlbumContext> options) : base(options)
        {
        }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var testData = Band.FakerData.Generate(10).ToList();
            modelBuilder.Entity<Band>().HasData(
            new Band()
            {
                Id = Guid.Parse("0F3E64F2-0BD4-9431-6024-8ABD724F5377"),
                Name = "Hunter Rivera",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Farley, Aspen V.",
            },
            new Band
            {
                Id = Guid.Parse("F5E1191D-B972-B37D-64B2-6E93D27DA92E"),
                Name = "Burton Chambers",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Huff, Darius M.",
            },
            new Band
            {
                Id = Guid.Parse("22C52AD2-AE11-036F-5D51-83369AA777BB"),
                Name = "Talon Bailey",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Whitney, Jena O.",
            },
            new Band
            {
                Id = Guid.Parse("0E66EA98-0BC8-214F-F2B4-4A36A4D5329F"),
                Name = "Gage Gross",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Daniels, Melvin Y.",
            },
            new Band
            {
                Id = Guid.Parse("9F5D793B-1EB9-BACA-281C-50AF5E5CE583"),
                Name = "Lars Patel",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Huber, Addison M.",
            },
            new Band
            {
                Id = Guid.Parse("AD706A51-ACB1-006E-26A3-6345F47A66DF"),
                Name = "Ethan Bryan",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Russo, Cruz Q.",
            },
            new Band
            {
                Id = Guid.Parse("B5B8C87F-EC11-2F49-C9ED-6A916F7DA923"),
                Name = "Cole Pickett",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Elliott, Harrison R.",
            },
            new Band
            {
                Id = Guid.Parse("7697D932-E00D-F9E5-ECC0-013E68259504"),
                Name = "Abdul Mclean",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Cleveland, Rooney F.",
            },
            new Band
            {
                Id = Guid.Parse("A695C383-F13C-4013-3D54-724BF36283F4"),
                Name = "Fletcher England",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Bridges, Mark D.",
            });

            modelBuilder.Entity<Album>().HasData(
                new Album
                {
                    Id = Guid.Parse("ade75b2b-78f9-4039-b07f-ee7dfb98d52d"),
                    Title = "Burton Chambers",
                    Description = "Awesome debut album AHAHHSHD",
                    BandId = Guid.Parse("A695C383-F13C-4013-3D54-724BF36283F4"),
                },
                new Album
                {
                    Id = Guid.Parse("d03563f8-9009-409e-98dc-2b68394353bc"),
                    Title = "Burton Chambers",
                    Description = "Awesome debut album AHAHHSHD",
                    BandId = Guid.Parse("A695C383-F13C-4013-3D54-724BF36283F4"),
                },
                new Album
                {
                    Id = Guid.Parse("5ce28d03-3254-4fdb-bbd7-414825b99fe4"),
                    Title = "Burton Chambers",
                    Description = "Awesome debut album AHAHHSHD",
                    BandId = Guid.Parse("A695C383-F13C-4013-3D54-724BF36283F4"),
                },
                new Album
                {
                    Id = Guid.Parse("71d4dcec-b4fe-4600-92ed-cc8fa6145645"),
                    Title = "Burton Chambers",
                    Description = "Awesome debut album AHAHHSHD",
                    BandId = Guid.Parse("A695C383-F13C-4013-3D54-724BF36283F4"),
                },
                new Album
                {
                    Id = Guid.Parse("AD706A51-ACB1-006E-26A3-6345F47A66DF"),
                    Title = "Burton Chambers",
                    Description = "Awesome debut album AHAHHSHD",
                    BandId = Guid.Parse("A695C383-F13C-4013-3D54-724BF36283F4"),
                },
                new Album
                {
                    Id = Guid.Parse("05b623b5-5b97-49d3-9808-5ee0af52c4aa"),
                    Title = "Burton Chambers",
                    Description = "Awesome debut album AHAHHSHD",
                    BandId = Guid.Parse("A695C383-F13C-4013-3D54-724BF36283F4"),
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
