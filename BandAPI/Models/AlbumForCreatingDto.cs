using BandAPI.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    [TitleAndDescriptionAttribute]
    public class AlbumForCreatingDto //: IValidatableObject
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }

        // validation in classe
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title.Equals(Description))
        //    {
        //        yield return new ValidationResult("The title and description must be different!", new[] {"AlbumForCreatingDto"});
        //    }
        //}
    }
}
