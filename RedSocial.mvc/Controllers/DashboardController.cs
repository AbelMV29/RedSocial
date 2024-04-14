using Microsoft.AspNetCore.Mvc;
using RedSocial.mvc.Extension;
using RedSocial.mvc.Interfaces;
using RedSocial.mvc.Models;
using RedSocial.mvc.ViewModels.Dashboard;

namespace RedSocial.mvc.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProfileUserRepository _profileUserRepository;
        private readonly IPhotoService _photoService;
        private readonly IPostRepository _postRepository;

        public DashboardController(IProfileUserRepository profileUserRepository, IPhotoService photoService
            ,IPostRepository postRepository)
        {
            _profileUserRepository = profileUserRepository;
            _photoService = photoService;
            _postRepository = postRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model) 
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var actualUser = await _profileUserRepository.GetByIdentityUserId(User.GetIdActualIdentityUser());
            string? stringImageUrl = null;
            if(model.Image is not null && model.Image.Length>0)
            {
                var photoAddResult = await _photoService.AddPhotoAsync(model.Image);
                stringImageUrl = photoAddResult.Url.ToString();
            }
            var newPost = new Post()
            {
                Title = model.Title,
                Body = model.Body,
                ImageUrl = stringImageUrl,
                ProfileUserId = actualUser.ProfileUserId,
                ProfileUser = actualUser,
                Created = DateTime.Now
            };
            var addDbPostResult = await _postRepository.Add(newPost);
            if (!addDbPostResult)
            {
                return View(model);
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
