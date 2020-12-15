using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus;

namespace BandAPI.Entities
{
    public class Album
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }

        [ForeignKey("BandId")]
        public Band Band { get; set; }
        public Guid BandId { get; set; }
        //public static Faker<Album> FakerData { get; } =
        //new Faker<Album>()
        //            .RuleFor(p => p.Id, f => Guid.NewGuid())
        //            .RuleFor(p => p.Title, f => f.Name.FirstName())
        //            .RuleFor(p => p.Description, f => f.Name.JobDescriptor());
    }
}