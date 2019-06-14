﻿using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {
        // GET: Gigs
        private ApplicationDbContext _context;

            public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(ViewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel = new GigFormViewModel
                {
                    Genres = _context.Genres.ToList()
                };
                return View("Create", viewModel);
            }
            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}