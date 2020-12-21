using BandAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


/*
 Custom Attribute per validare i dati, in alternativa alla validatione nella classe
 */
namespace BandAPI.ValidationAttributes
{
    public class TitleAndDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var album = (AlbumManipulationDto)validationContext.ObjectInstance;

            if(album.Title == album.Description)
            {
                return new ValidationResult("The title and the description must be different!", new[] { "AlbumManipulationDto" });
            }

            return ValidationResult.Success;
        }
    }
}
