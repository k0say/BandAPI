using Microsoft.AspNetCore.Mvc;
using BandAPI.Services;
using System;
using System.Collections.Generic;
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
        
        [HttpGet(Name = "GetBands")]
        //HEAD -> come get ma non ritorna un response body (utiler per sapere se una risposta è stata modifica, o valida)
        [HttpHead]
        public ActionResult<IEnumerable<BandDto>> GetBands([FromQuery] BandsResourceParameters bandsResourceParameters)
        {
            var bandsFromRepo = _bandAlbumRepository.GetBands(bandsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo));
        }
        
        [HttpGet("{bandId}", Name="GetBand")]
        public IActionResult GetBand(Guid bandId)
        {
            var bandFromRepo = _bandAlbumRepository.GetBand(bandId);
            if (bandFromRepo == null)
                return NotFound();

            return Ok(bandFromRepo);
        }

        [HttpPost]
        public ActionResult<BandDto> CreateBand([FromBody] BandForCreatingDto band) //FromBody quando è tipo complesso
        {
            //ritorna BadRequest se è null
            var bandEntity = _mapper.Map<Entities.Band>(band);
            _bandAlbumRepository.AddBand(bandEntity);
            _bandAlbumRepository.Save();

            var bandToReturn = _mapper.Map<BandDto>(bandEntity);

            return CreatedAtRoute("GetBand", new { bandId = bandToReturn.Id }, bandToReturn);
        }

        [HttpOptions]
        public IActionResult GetBandsOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }

        [HttpDelete("{bandId}")]
        public ActionResult DeleteBand(Guid bandId)
        {
            var bandFromRepo = _bandAlbumRepository.GetBand(bandId);

            if (bandFromRepo == null)
                return NotFound();
            //rimuove la band e i child (albums)
            _bandAlbumRepository.DeleteBand(bandFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }

        private string CreateBandsUri(BandsResourceParameters bandsResourceParameters, UriType uriType)
        {
            switch (uriType)
            {
                case UriType.PreviousPage:
                    return Url.Link("GetBands", new
                        {
                            pageNumber = bandsResourceParameters.PageNumber - 1,
                            pageSize = bandsResourceParameters.PageSize,
                            mainGenre = bandsResourceParameters.MainGenre,
                            searchQuery = bandsResourceParameters.SearchQuery
                        });
                case UriType.NextPage:
                    return Url.Link("GetBands", new
                    {
                        pageNumber = bandsResourceParameters.PageNumber + 1,
                        pageSize = bandsResourceParameters.PageSize,
                        mainGenre = bandsResourceParameters.MainGenre,
                        searchQuery = bandsResourceParameters.SearchQuery
                    });
                default:
                    return Url.Link("GetBands", new
                    {
                        pageNumber = bandsResourceParameters.PageNumber,
                        pageSize = bandsResourceParameters.PageSize,
                        mainGenre = bandsResourceParameters.MainGenre,
                        searchQuery = bandsResourceParameters.SearchQuery
                    });
            }
        }

    }
}
