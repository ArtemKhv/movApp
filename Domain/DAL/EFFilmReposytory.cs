using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using movApp.Models;
using System;
using System.Collections.Generic;
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


    }
}
