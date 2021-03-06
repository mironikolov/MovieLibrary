﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
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


        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name="Shrek" };
            var customers = new List<Customer>
            {
                new Customer{ Name = "Customer1" },
                new Customer{ Name = "Customer2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View( viewModel );
        }

        public ActionResult Index()
        {
            var movies = _context.Movies.Include( movie => movie.Genre ).ToList();
            var viewModel = new MoviesListViewModel
            {
                Movies = movies
            };

            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View( "List", viewModel );
            }

            return View( "ReadOnlyList", viewModel );

        }

        [Route( "movies/details/{id}")]
        public ActionResult Details( int id)
        {
            var movies = _context.Movies.Include( movie => movie.Genre ).ToList();

            var model = movies.Find( movie=> movie.Id == id);
            return View( model );
        }

        [Route( "movies/released/{year}/{month:regex(\\d{4}):range(1,12)}" )]
        public ActionResult ByReleaseDate( int year, int month )
        {

            return Content( year + "/" + month );
        }

        [Authorize( Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var movieFormViewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };
            return View( "MovieForm", movieFormViewModel );
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save( Movie movie )
        {
            if ( !ModelState.IsValid )
            {
                var viewModel = new MovieFormViewModel( movie )
                {
                    Genres = _context.Genres.ToList()
                };
                return View( "MovieForm", viewModel );
            }

            if ( movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add( movie );
            }
            else
            {
                var movieInDb = _context.Movies.Single( m => m.Id == movie.Id );
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();

            return RedirectToAction( "Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit( int id )
        {
            var movie = _context.Movies.SingleOrDefault( m => m.Id == id );
            if ( movie == null )
            {
                return HttpNotFound();
            }

            var movieFormViewModel = new MovieFormViewModel( movie )
            {
                Genres = _context.Genres.ToList()
            };

            return View( "MovieForm", movieFormViewModel );
        }
    }
}