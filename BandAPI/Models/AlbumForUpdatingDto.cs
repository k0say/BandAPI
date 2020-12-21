using BandAPI.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    [TitleAndDescription(ErrorMessage = "Title must be different from description")]
    public class AlbumForUpdatingDto
    {

        [Required(ErrorMessage = "The title need to be filled in")]
        [MaxLength(200, ErrorMessage = "Title needs to be up to 200 characters")]
        public string Title { get; set; }
        [Required]
        [MaxLength(400, ErrorMessage = "Description to be up to 400 characters")]
        public string Description { get; set; }
    }
}
