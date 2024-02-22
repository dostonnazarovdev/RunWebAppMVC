using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interface;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using RunGroopWebApp.Services;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        public RaceController(IRaceRepository repository,IPhotoService photoService)
        {
            _raceRepository = repository;
            _photoService = photoService;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(CreateRaceViewModel raceMV)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceMV.Image);

                var race = new Race
                {
                    Title = raceMV.Title,
                    Description = raceMV.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceMV.Address.Street,
                        City = raceMV.Address.City,
                        State = raceMV.Address.State
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(raceMV);
        }
    }
}
