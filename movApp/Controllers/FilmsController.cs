using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using movApp.Models;

namespace movApp.Controllers
{
    public class FilmsController : Controller
    {
        private readonly IFilm _film;
        IWebHostEnvironment _appEnvironment;

        public FilmsController(IFilm film, IWebHostEnvironment appEnvironment)
        {
            _film = film;
            _appEnvironment = appEnvironment;
        }

        // GET: Films
        public IActionResult Index()
        {
            return View();
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _film.GetFilm(id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewBag.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Year,Director,PathImg")] Film film, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {

                if (uploadedFile != null)
                {
                    string path = "/img/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    film.PathImg = path;
                }
                
                film.UserId = Request.Form["UserID"];
                await _film.Add(film);
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var film = await _film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            if (film.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier)) 
            {
                 return RedirectToAction("ErrorEdit"); 
            }
            ViewBag.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(film);
        }

        public IActionResult ErrorEdit() 
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Year,Director,PathImg")] Film film, IFormFile uploadedFile)
        {
            if (id != film.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {

                    if (uploadedFile != null)
                    {
                        string path = "/img/" + uploadedFile.FileName;
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        film.PathImg = path;
                    }
                    film.UserId = Request.Form["UserID"];
                    await _film.Edit(film);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _film.GetFilm(id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _film.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _film.FilmExists(id);
        }


        [HttpPost]
        [Route("Films/GetFilmsAjaxAsync")]
        public async Task<IActionResult> GetFilmsAjaxAsync()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();
            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc, desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var films = await _film.GetFilms();

            var customerData = films.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                year = x.Year 
            });

            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(x => x.name.ToLower().Contains(searchValue.ToLower()));
            }

            // сортивка по столбцу(sortColumn) и порядок сортировки (sortColumnDirection)
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                if (sortColumnDirection == "desc")
                {
                    switch (sortColumn)
                    {

                        case "":
                            customerData = customerData.OrderBy(x => x.name);
                            break;
                        case "Year":
                            customerData = customerData.OrderBy(x => x.year);
                            break;
                    }
                }
                else if (sortColumnDirection == "asc")
                {
                    switch (sortColumn)
                    {

                        case "":
                            customerData = customerData.OrderByDescending(x => x.name);
                            break;
                        case "Year":
                            customerData = customerData.OrderByDescending(x => x.year);
                            break;
                    }
                }
            }

            //total number of rows count   
            recordsTotal = customerData.Count();
            //Paging   
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data
            return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }


    }
}
