using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interface;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Clubs.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(i=>i.Address).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(p => p.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool Update(Club club)
        {
       _context.Update(club);
            return Save();
        }
    }
}
