using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interface;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private IRaceRepository _raceRepository;
        public RaceController(IRaceRepository repository)
        {
            _raceRepository = repository;
        }
        public async  Task<IActionResult> Index()
        {
           var races = await _raceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _raceRepository.GetByIdAsync(id);
            return View(detail);
        }
    }
}
