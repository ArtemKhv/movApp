using movApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore;

using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IFilm
    {
        Task<IEnumerable<Film>> GetFilms();

        Task<Film> GetFilm(int? id);

        Task Add(Film film);

        Task<Film> FindAsync(int? id);

        Task Edit(Film film);

        Task Delete(int id);

        bool FilmExists(int id);

    }
}
