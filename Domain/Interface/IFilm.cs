using movApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IFilm
    {
        Task<IEnumerable<Film>> GetFilms();

        Task<Film> GetFilm(int? id);

            
    }
}
