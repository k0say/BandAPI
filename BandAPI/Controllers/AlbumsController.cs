﻿using AutoMapper;
using BandAPI.Models;
using BandAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Controllers
{
    [ApiController]
    [Route("api/bands/{bandId}/albums")]
    public class AlbumsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ?? throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumsDto>> GetAlbumsForBand(Guid bandId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumsFromRepo = _bandAlbumRepository.GetAlbums(bandId);
            return Ok(_mapper.Map<IEnumerable<AlbumsDto>>(albumsFromRepo));
        }

        [HttpGet("{albumId}", Name="GetAlbumForBand")]
        public ActionResult<AlbumsDto> GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            if (!_bandAlbumRepository.AlbumExists(albumId))
                return NotFound();

            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            
            if (albumFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<AlbumsDto>(albumFromRepo));
        }

        [HttpPost]
        public ActionResult<AlbumsDto> CreateAlbumForBand(Guid bandId, [FromBody] AlbumForCreatingDto album)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
            {
                return NotFound();
            }

            var albumEntity = _mapper.Map<Entities.Album>(album);
            _bandAlbumRepository.AddAlbum(bandId, albumEntity);
            _bandAlbumRepository.Save();

            var albumToReturn = _mapper.Map<AlbumsDto>(albumEntity);
            return CreatedAtRoute("GetAlbumForBand", new { bandId = bandId, albumId = albumToReturn.Id}, albumToReturn);
        }

        [HttpPut("{albumId}")]
        public ActionResult UpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] AlbumForUpdatingDto album)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumFromRepo == null)
            {
                var albumToAdd = _mapper.Map<Entities.Album>(album);
                albumToAdd.Id = albumId;
                _bandAlbumRepository.AddAlbum(bandId, albumToAdd);
                _bandAlbumRepository.Save();

                var albumToReturn = _mapper.Map<AlbumsDto>(albumToAdd);

                return CreatedAtRoute("GetAlbumForBand", new { bandId = bandId, albumId = albumToReturn.Id }, albumToReturn);
            }

            _mapper.Map(album, albumFromRepo);
            _bandAlbumRepository.UpdateAlbum(albumFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }

        //JsonPatchDocument è la libreria da utilizzare per utilizzare PATCH
        /*
         il formato del content è di questo tipo:
            [
                {
                    "op": "replace", oppure "op": "remove" | "op": "add" ... vi sono molte operazioni disponibili
                    "path": "/title",
                    "value": "updated title"
                }
            ]
         */
        [HttpPatch("{albumId}")]
        public ActionResult PartiallyUpdateAlbumForBand(Guid bandId, Guid albumId, [FromBody] JsonPatchDocument<AlbumForUpdatingDto> patchDocument)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumFromRepo == null)
            {
                var albumDto = new AlbumForUpdatingDto();
                patchDocument.ApplyTo(albumDto);
                var albumToAdd = _mapper.Map<Entities.Album>(albumDto);
                albumToAdd.Id = albumId;
                _bandAlbumRepository.AddAlbum(bandId, albumToAdd);
                _bandAlbumRepository.Save();

                var albumToReturn = _mapper.Map<AlbumsDto>(albumToAdd);

                return CreatedAtRoute("GetAlbumForBand", new { bandId = bandId, albumId = albumToReturn.Id }, albumToReturn);
            }

            //performiamo il PATCH sul DTO piuttosto che sull'entità come per PUT
            var albumToPatch = _mapper.Map<AlbumForUpdatingDto>(albumFromRepo);
            patchDocument.ApplyTo(albumToPatch, ModelState);

            //performiamo la validazione per il PATCH
            if (!TryValidateModel(albumToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(albumToPatch, albumFromRepo);
            _bandAlbumRepository.UpdateAlbum(albumFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }

        [HttpDelete("{albumId}")]
        public ActionResult DeleteAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();

            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);

            if (albumFromRepo == null)
                return NotFound();

            _bandAlbumRepository.DeleteAlbum(albumFromRepo);
            _bandAlbumRepository.Save();

            return NoContent();
        }

        [HttpGet("/allalbums")]
        //[Route("/allalbums")]
        public ActionResult<IEnumerable<AlbumsDto>> GetAll()
        {
            var all = _bandAlbumRepository.GetAllAlbums();
            return Ok(_mapper.Map<IEnumerable<AlbumsDto>>(all));
        }

    }
}
