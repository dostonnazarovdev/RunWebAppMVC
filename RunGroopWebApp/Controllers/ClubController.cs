using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interface;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        public ClubController(IClubRepository repository)
        {
            _clubRepository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _clubRepository.GetByIdAsync(id);
            return View(detail);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
