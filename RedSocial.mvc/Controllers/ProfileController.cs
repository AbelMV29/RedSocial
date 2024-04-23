using Microsoft.AspNetCore.Mvc;
using RedSocial.mvc.Extension;
using RedSocial.mvc.Interfaces;
using RedSocial.mvc.ViewModels.Profile;

namespace RedSocial.mvc.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileUserRepository _profileUserRepository;
        private readonly IPhotoService _photoService;

        public ProfileController(IProfileUserRepository profileUserRepository,IPhotoService photoService)
        {
            _profileUserRepository = profileUserRepository;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NewUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewUser(NewUserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var profileUser = await _profileUserRepository.GetByIdentityUserId(User.GetIdActualIdentityUser());
            profileUser.Description = model.Description;
            profileUser.IsComplete = true;
            if (model is not null && model.Image.Length>0)
            {
               var addImageResult =  await _photoService.AddPhotoAsync(model.Image);
                var newImageUrl = addImageResult.Url.ToString();

                profileUser.ImageUrl = newImageUrl;
            }
            var updateResult = await _profileUserRepository.Update(profileUser);
            if(!updateResult)
            {
                TempData["Error"] = "Error al completar tu perfil";
                return View(model);
            }
            return RedirectToAction("Index","Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> ProfileDetails(int idProfile)
        {
            var profileUser = await _profileUserRepository.GetById(idProfile);
            var profileDetailViewModel = new ProfileDetailViewModel()
            {
                Name = profileUser.Name,
                LastName = profileUser.LastName,
                Description = profileUser.Description,
                ImageUrl = profileUser.ImageUrl,
                Created = profileUser.Created,
                Email = profileUser.IdentityUser.Email,
                UserName = profileUser.IdentityUser.UserName
            };
            return View(profileDetailViewModel);
        }
    }
}
