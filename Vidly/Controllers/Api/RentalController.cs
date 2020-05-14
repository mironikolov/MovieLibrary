using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using AutoMapper;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public IHttpActionResult CreateRental( RentalDto rentalDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Empty values");
            }

            var customer = _context.Customers.SingleOrDefault( c => c.Id == rentalDto.CustomerId );
            if (customer == null)
            {
                return BadRequest("CustomerId not valid");
            }

            var movies = _context.Movies.Where( m => rentalDto.MovieIds.Contains( m.Id ) ).ToList();
            if (movies.Count != rentalDto.MovieIds.Count )
            {
                return BadRequest("One or more MovieIds are not valid");
            }

            foreach (var movie in movies )
            {
                if (movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available");
                }
                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add( rental );
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
