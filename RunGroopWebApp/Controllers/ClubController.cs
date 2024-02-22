using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interface;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;
using System.Diagnostics.Eventing.Reader;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        public ClubController(IClubRepository repository, IPhotoService photoService)
        {
            _clubRepository = repository;
            _photoService = photoService;
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



        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            var result = await _photoService.AddPhotoAsync(clubVM.Image);
            if (ModelState.IsValid)
            {
                var clubs = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    }
                };

                _clubRepository.Add(clubs);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error occurred while uploading photo");
            }
                return View(clubVM);
           }
        }
    }
