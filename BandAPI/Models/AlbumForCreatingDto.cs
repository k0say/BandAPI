﻿using BandAPI.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    //Custom Attribute svilppato in TitleAndDescriptionAttribute
    [TitleAndDescription(ErrorMessage = "Title must be different from description")]
    public class AlbumForCreatingDto //: IValidatableObject
    {
        [Required(ErrorMessage = "The title need to be filled in")]
        [MaxLength(200, ErrorMessage = "Title needs to be up to 200 characters")]
        public string Title { get; set; }
        [MaxLength(400, ErrorMessage = "Description to be up to 400 characters")]
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
