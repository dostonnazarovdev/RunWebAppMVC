using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interface
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<IEnumerable<Race>> GetRaceByCity(string city);
        bool Add(Race race);
        bool Delete(Race race);
        bool Update(Race race);
        bool Save();
    }
}
