using Bogus;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Entities
{
    public class Band
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime Founded { get; set; }
        [Required]
        [MaxLength(50)]
        public string MainGenre { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();


        //public static Faker<Band> FakerData { get; } =
        //    new Faker<Band>()
        //        .RuleFor(p => p.Id, f => Guid.NewGuid())
        //        .RuleFor(p => p.Name, f => f.Name.FirstName())
        //        .RuleFor(p => p.Founded, f => f.Date.Past(20))
        //        .RuleFor(p => p.MainGenre, f => f.Music.Genre());

    }
}
