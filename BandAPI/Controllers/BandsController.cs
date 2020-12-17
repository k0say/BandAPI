using Microsoft.AspNetCore.Mvc;
using BandAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BandAPI.Models;
using BandAPI.Helpers;
using AutoMapper;

namespace BandAPI.Controllers
{
    [ApiController]
    [Route("api/bands")]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet]
        //HEAD -> come get ma non ritorna un response body (utiler per sapere se una risposta è stata modifica, o valida)
        [HttpHead]
        public ActionResult<IEnumerable<BandDto>> GetBands([FromQuery]string mainGenre, [FromQuery] string searchQuery)
        {
            var bandsFromRepo = _bandAlbumRepository.GetBands(mainGenre, searchQuery);
            return Ok(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo));
        }
        
        [HttpGet("{bandId}")]
        public IActionResult GetBand(Guid bandId)
        {
            var bandFromRepo = _bandAlbumRepository.GetBand(bandId);
            if (bandFromRepo == null)
                return NotFound();

            return Ok(bandFromRepo);
        }

    }
}
