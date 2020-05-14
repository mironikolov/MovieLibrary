﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using AutoMapper;
using Vidly.Dtos;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/movies
        [HttpGet]
        public IHttpActionResult GetMovies( string query = null )
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre);

            if (!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query) && m.NumberAvailable > 0 );
            }

            var movieDtos = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok( movieDtos );
        }

        //GET /api/movies/1
        [HttpGet]
        public IHttpActionResult GetMovie( int id )
        {
            var movie = _context.Movies.SingleOrDefault( m => m.Id == id );
            if ( movie == null )
            {
                return NotFound();
            }

            return Ok( Mapper.Map< Movie, MovieDto>( movie ) );
        }

        //POST /api/movie
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public IHttpActionResult CreateMovie( MovieDto movieDto )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>( movieDto );
            _context.Movies.Add( movie );
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created( new Uri( Request.RequestUri + "/" + movie.Id ), movieDto );
        }

        //PUT /api/movie/1
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut]
        public IHttpActionResult UpdateMovie( int id, MovieDto movieDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            Mapper.Map( movieDto, movieInDb );
            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/movie/1
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpDelete]
        public IHttpActionResult DeleteMovie( int id )
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            _context.Movies.Remove( movieInDb );
            _context.SaveChanges();

            return Ok();
        }
    }
}
