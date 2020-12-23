using Microsoft.AspNetCore.Mvc;
using BandAPI.Services;
using System;
using System.Collections.Generic;
using BandAPI.Models;
using BandAPI.Helpers;
using AutoMapper;
using System.Text.Json;
using BandAPI.Entities;

namespace BandAPI.Controllers
{
    [ApiController]
    [Route("api/bands")]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyValidationService _propertyValidationService;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper, IPropertyMappingService propertyMappingService, IPropertyValidationService propertyValidationService)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyValidationService = propertyValidationService ?? throw new ArgumentNullException(nameof(propertyValidationService));
        }

        [HttpGet(Name = "GetBands")]
        //HEAD -> come get ma non ritorna un response body (utiler per sapere se una risposta è stata modifica, o valida)
        [HttpHead]
        public IActionResult GetBands([FromQuery] BandsResourceParameters bandsResourceParameters)
        {
            if (!_propertyMappingService.ValidMappingExists<BandDto, Band>(bandsResourceParameters.OrderBy))
                return BadRequest();

            if (!_propertyValidationService.HasValidProperties<BandDto>(bandsResourceParameters.Fields))
                return BadRequest();

            var bandsFromRepo = _bandAlbumRepository.GetBands(bandsResourceParameters);

            var previousPageLink = bandsFromRepo.HasPrevious ? CreateBandsUri(bandsResourceParameters, UriType.PreviousPage) : null;

            var nextPageLink = bandsFromRepo.HasNext ? CreateBandsUri(bandsResourceParameters, UriType.NextPage) : null;

            var metaData = new
            {
                totalCount = bandsFromRepo.TotalCount,
                pageSize = bandsFromRepo.PageSize,
                currentPage = bandsFromRepo.CurrentPage,
                totalPages = bandsFromRepo.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData));

            return Ok(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo).ShapeData(bandsResourceParameters.Fields));
        }

        [HttpGet("{bandId}", Name = "GetBand")]
        public IActionResult GetBand(Guid bandId, string fields)
        {
            if (!_propertyValidationService.HasValidProperties<BandDto>(fields))
                return BadRequest();

            var bandFromRepo = _bandAlbumRepository.GetBand(bandId);

            if (bandFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<BandDto>(bandFromRepo).ShapeData(fields));
        }

        [HttpPost]
        public ActionResult<BandDto> CreateBand([FromBody] BandForCreatingDto band) //FromBody quando è tipo complesso
        {
            //ritorna BadRequest se è null
            var bandEntity = _mapper.Map<Band>(band);
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
                        fields = bandsResourceParameters.Fields,
                        orderBy = bandsResourceParameters.OrderBy,
                        pageNumber = bandsResourceParameters.PageNumber - 1,
                        pageSize = bandsResourceParameters.PageSize,
                        mainGenre = bandsResourceParameters.MainGenre,
                        searchQuery = bandsResourceParameters.SearchQuery
                    });
                case UriType.NextPage:
                    return Url.Link("GetBands", new
                    {
                        fields = bandsResourceParameters.Fields,
                        orderBy = bandsResourceParameters.OrderBy,
                        pageNumber = bandsResourceParameters.PageNumber + 1,
                        pageSize = bandsResourceParameters.PageSize,
                        mainGenre = bandsResourceParameters.MainGenre,
                        searchQuery = bandsResourceParameters.SearchQuery
                    });
                default:
                    return Url.Link("GetBands", new
                    {
                        fields = bandsResourceParameters.Fields,
                        orderBy = bandsResourceParameters.OrderBy,
                        pageNumber = bandsResourceParameters.PageNumber,
                        pageSize = bandsResourceParameters.PageSize,
                        mainGenre = bandsResourceParameters.MainGenre,
                        searchQuery = bandsResourceParameters.SearchQuery
                    });
            }
        }

    }
}
