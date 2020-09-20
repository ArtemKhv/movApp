using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using movApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL
{
    public class EFFilmReposytory : IFilm
    {
        private readonly MovieDbContext _db = new MovieDbContext();

        public async Task<IEnumerable<Film>> GetFilms()
        {
            return await _db.Films.ToListAsync();
        }

        public async Task<Film> GetFilm(int? id)
        {
            return await _db.Films.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(Film film)
        {
            _db.Films.Add(film);
            await _db.SaveChangesAsync();
        }

        public async Task<Film> FindAsync(int? id)
        {
            return await _db.Films.FindAsync(id);
        }

        public async Task Edit(Film film)
        {
            _db.Films.Update(film);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var film = await _db.Films.FindAsync(id);
            _db.Films.Remove(film);
            await _db.SaveChangesAsync();
        }

        public bool FilmExists(int id)
        {
            return _db.Films.Any(e => e.Id == id);
        }
    }
}
