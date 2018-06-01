using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(x => x.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(x => x.Genre).SingleOrDefault(x => x.Id == id);

            if(movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult Save(Movie movie)
        {
            if( movie.Id == 0 )
            {
                _context.Movies.Add( movie );
            }
            else
            {
                var movieInDb = _context.Movies.Single( x => x.Id == movie.Id );

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction( "Index", "Movies" );
        }

        public ActionResult Edit( int id )
        {
            var movie = _context.Movies.SingleOrDefault( x => x.Id == id );

            if( movie == null )
                return HttpNotFound();

            var viewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View( "MovieForm", viewModel );
        }
    }
}